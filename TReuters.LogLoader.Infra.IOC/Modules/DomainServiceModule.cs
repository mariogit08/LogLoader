using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.DomainServices;
using TReuters.LogLoader.Domain.Interfaces.DomainServices;

namespace TReuters.LogLoader.Infra.IOC.Modules
{
    public static class DomainServiceModule
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ILogDomainService, LogDomainService>();
        }
    }
}
