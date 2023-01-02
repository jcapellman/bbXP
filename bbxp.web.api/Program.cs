using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.web.api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

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
                builder.Services.AddSwaggerGen();

                builder.Services.AddMemoryCache();
                builder.Services.AddDbContext<bbxpDbContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(bbxpDbContext))));

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiConfig.JWTSecret))
                    };
                });

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseDeveloperExceptionPage();
                    app.UseSwaggerUI();
                }

                using var scope = app.Services.CreateScope();

                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<bbxpDbContext>();
                    db.Database.Migrate();
                } catch (Exception dbex) {
                    logger.Error(dbex, "Failed to run database migrations due to an exception");
                }

                app.UseCors("MyPolicy");

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseRouting();
                app.UseAuthorization();

                app.MapControllers();

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