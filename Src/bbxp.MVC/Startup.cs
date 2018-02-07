using bbxp.MVC.Middleware;
using bbxp.lib.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace bbxp.MVC {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.Configure<GlobalSettings>(Configuration.GetSection("GlobalSettings"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
        //    app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
        //    app.UseHTTPRequestLogger();

            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}