using bbxp.web.api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

using bbxp.lib.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace bbxp.web.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("bbxp.web.api starting up...");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Configuration.AddEnvironmentVariables();

                var apiConfig = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

                if (apiConfig != null)
                {
                    builder.Services.AddSingleton(apiConfig);
                }

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "MyPolicy",
                                policy =>
                                {
                                    policy.AllowAnyOrigin().AllowAnyMethod();
                                });
                });

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "JWT Authentication",
                        Description = "Enter JWT Bearer token **_only_**",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    };
                    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                    var securitySchemeReference = new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme);
                    c.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
                    {
                        {securitySchemeReference, new List<string>()}
                    });
                });

                builder.Services.AddMemoryCache();

                // Response compression for reduced payload sizes (60-80% reduction for JSON)
                builder.Services.AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.Providers.Add<GzipCompressionProvider>();
                    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                        ["application/json", "text/json", "application/xml", "text/xml"]);
                });

                builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Fastest; // Balance between compression ratio and CPU
                });

                builder.Services.Configure<GzipCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Fastest;
                });

                builder.Services.AddDbContext<BbxpContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BbxpContext)), b => b.MigrationsAssembly("bbxp.web.api")));

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = apiConfig?.JWTIssuer,
                        ValidAudience = apiConfig?.JWTAudience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiConfig?.JWTSecret ?? throw new NullReferenceException("JWTSecret was null")))
                    };
                });

                var app = builder.Build();

                var scope = app.Services.CreateScope();

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<BbxpContext>();
                    db.Database.Migrate();
                }
                catch (Exception dbex)
                {
                    logger.Error(dbex, "Failed to run database migrations due to an exception");
                }

                app.UseCors("MyPolicy");

                // Enable response compression (must be before other middleware that write responses)
                app.UseResponseCompression();

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseRouting();
                app.UseAuthorization();

                app.MapControllers();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseDeveloperExceptionPage();
                    app.UseSwaggerUI();
                }

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "bbxp.web.api failed to startup properly because of exception");

                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}