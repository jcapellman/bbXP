using System;
using System.Threading.Tasks;

using bbxp.MVC.Entities;
using bbxp.MVC.Entities.Objects.Table;

using Microsoft.AspNetCore.Http;

namespace bbxp.MVC.Middleware {
    public class HTTPRequestLogger {
        private readonly RequestDelegate _next;

        public HTTPRequestLogger(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            using (var eFactory = new EntityFactory()) {
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