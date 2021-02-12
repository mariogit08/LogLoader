using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Infra.Data.UnitOfWork;
using TReuters.LogLoader.Infra.IOC.Modules;

namespace TReuters.LogLoader.Infra.IOC
{
    public static class IOCBootstrapper
    {
        public static void RegisterAllIOCModules(this IServiceCollection services)
        {
            services.RegisterApplications();
            services.RegisterContexts();
            services.RegisterDomainServices();
            services.RegisterServices();
            services.RegisterRepositories();
        }
    }
}
