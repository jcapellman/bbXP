using bbxp.lib.Database;

using Microsoft.EntityFrameworkCore;

using NLog;
using NLog.Web;

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

                var app = builder.Build();

             //   if (app.Environment.IsDevelopment())
               // {
                    app.UseSwagger();
                    app.UseDeveloperExceptionPage();
                    app.UseSwaggerUI();
                //}

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