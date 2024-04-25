using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using Repository.Implementations.Unit;
using Repository.Interfaces.Unit;

namespace Repository
{
    public static class RepositoryRegistration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddContext();
            services.AddScoped<IRepositoryUnit, RepositoryUnit>();
        }
    }
}
