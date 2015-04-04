using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

using bbXP.pcl.Transports.Global;

namespace bbXP.mvc {
    public class Startup {
        public Startup(IHostingEnvironment env) {
	        Configuration = new Configuration()
		        .AddJsonFile("config.json")
		        .AddJsonFile("bbxpconfig.json");
        }

        public IConfiguration Configuration { get; set; }

	    private T readConfig<T>() {
		    var tmpObject = Activator.CreateInstance<T>();

		    var objectType = tmpObject.GetType();
		    IList<PropertyInfo> props = new List<PropertyInfo>(objectType.GetProperties());

		    var className = objectType.Name;

		    foreach (var prop in props) {
			    var cfgValue = Configuration.Get(String.Format("{0}:{1}", className, prop.Name));

				prop.SetValue(tmpObject, cfgValue, null);
			}

			return tmpObject;
	    }
		
		public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();

			services.AddEntityFramework().AddSqlServer();

            services.AddWebApiConventions();

			var options = readConfig<GlobalVars>();

            services.AddSingleton(a => options);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory) {
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase)) {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            } else {
                app.UseErrorHandler("/Home/Error");
            }

            app.UseStaticFiles();
			
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
