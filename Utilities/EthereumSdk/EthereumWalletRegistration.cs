using EthereumSdk.Implementations;
using EthereumSdk.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EthereumSdk
{
    public static class EthereumSdkRegistration
    {
        public static void AddEthereumSdk(this IServiceCollection services)
        {
            services.AddScoped<IEtherService, EtherService>();
        }

        public static IServiceCollection AddEthereumSdkWithReturn(this IServiceCollection services)
        {
            services.AddScoped<IEtherService, EtherService>();

            return services;
        }
    }
}
