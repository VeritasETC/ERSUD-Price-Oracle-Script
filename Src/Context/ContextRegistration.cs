using Microsoft.Extensions.DependencyInjection;

namespace Context
{
    public static class ContextRegistration
    {

        public static void AddContext(this IServiceCollection services)
        {
            services.AddDbContext<ExchnageContext>();
        }
    }
}
