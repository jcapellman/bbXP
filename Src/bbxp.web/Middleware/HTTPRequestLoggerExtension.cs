using Microsoft.AspNetCore.Builder;

namespace bbxp.web.Middleware {
    public static class HTTPRequestLoggerExtension {
        public static IApplicationBuilder UseHTTPRequestLogger(this IApplicationBuilder builder) {
            return builder.UseMiddleware<HTTPRequestLogger>();
        }
    }
}