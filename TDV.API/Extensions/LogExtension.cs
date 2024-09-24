using System.Runtime.CompilerServices;
using TDV.API.Middlewares;

namespace TDV.API.Extensions
{
    static public class LogExtension
    {
        public static IApplicationBuilder UseLog(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<Log>();
        }
    }
}
