using Microsoft.Extensions.DependencyInjection;
using Logger.Interfaces;
using Logger.Implementations;

namespace Logger
{
    public static class LoggerRegistration
    {
        public static void AddEventLogger(this IServiceCollection services)
        {
            services.AddScoped<IEventLogger, EventLoggerToFile>();
        }
    }
}
