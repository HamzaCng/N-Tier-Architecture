using NLog;
using LogLevel = NLog.LogLevel;

namespace TDV.API.Middlewares
{
    public class Log
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        RequestDelegate _next;
        public Log(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // IP Adresi
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();        

            // Kullanıcı ID (Kimlik doğrulama varsa)
            var userId = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Anonymous";

            // İstek tipi (GET, POST vb.)
            var requestType = httpContext.Request.Method;

            // İstek atılan endpoint
            var endpoint = httpContext.Request.Path;

            // Zaman
            var requestTime = DateTime.UtcNow;

            // Log verilerini NLog'a gönder
            var logEvent = new LogEventInfo(LogLevel.Warn, Logger.Name, "Request logged")
            {
                Properties =
                {
                    ["ipAddress"] = ipAddress,
                    ["userId"] = userId,
                    ["requestType"] = requestType,
                    ["endpoint"] = endpoint,
                    ["time"] = requestTime
                }
            };

            Logger.Log(logEvent);
            await _next.Invoke(httpContext);
        }
    }
}
