using bbxp.web.api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

using bbxp.lib.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {securityScheme, Array.Empty<string>()}
                    });
                });

                builder.Services.AddMemoryCache();
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