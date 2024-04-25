using Ethereum.Contract.Implementations;
using Ethereum.Contract.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract
{   
    public static class HandlerRegistration
    {
        public static void AddContract(this IServiceCollection services)
        {
            services.AddScoped<IContractHandler, ContractHandler>();
        }


        public static IServiceCollection AddContractWithReturn(this IServiceCollection services)
        {
            services.AddScoped<IContractHandler, ContractHandler>();

            return services;
        }
    }
}
