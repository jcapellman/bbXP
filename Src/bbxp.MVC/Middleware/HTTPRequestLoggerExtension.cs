using Microsoft.AspNetCore.Builder;

namespace bbxp.MVC.Middleware {
    public static class HTTPRequestLoggerExtension {
        public static IApplicationBuilder UseHTTPRequestLogger(this IApplicationBuilder builder) {
            return builder.UseMiddleware<HTTPRequestLogger>();
        }
    }
}