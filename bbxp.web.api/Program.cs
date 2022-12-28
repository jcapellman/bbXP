using bbxp.lib.Database;
using Microsoft.EntityFrameworkCore;

namespace bbxp.web.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("bbxpDbContext")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<bbxpDbContext>();
                    db.Database.Migrate();
                }
            } catch (Exception ex)
            {
                // TODO: Logging
                Console.WriteLine(ex.ToString());
            }

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}