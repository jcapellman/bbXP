using System.Threading.Tasks;

using bbxp.lib.Settings;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace bbxp.web.Middleware {
    public class HTTPRequestLogger {
        private readonly RequestDelegate _next;
        protected GlobalSettings _globalSettings;

        public HTTPRequestLogger(RequestDelegate next, IOptions<GlobalSettings> globalSettings) {
            _next = next;
            _globalSettings = globalSettings.Value;
        }
    }
}