using System;
using System.Threading.Tasks;

using bbxp.MVC.Entities;
using bbxp.MVC.Entities.Objects.Table;
using bbxp.MVC.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Middleware {
    public class HTTPRequestLogger {
        private readonly RequestDelegate _next;
        protected GlobalSettings _globalSettings;

        public HTTPRequestLogger(RequestDelegate next, IOptions<GlobalSettings> globalSettings) {
            _next = next;
            _globalSettings = globalSettings.Value;
        }

        public async Task Invoke(HttpContext context) {
            using (var eFactory = new EntityFactory(_globalSettings.DatabaseConnection)) {
                var request = new Requests {
                    Active = true,
                    Modified = DateTime.Now,
                    Created = DateTime.Now,
                    RequestStr = context.Request.Path
                };
                
                eFactory.Requests.Add(request);

                await eFactory.SaveChangesAsync();

                await _next.Invoke(context);
            }
        }
    }
}