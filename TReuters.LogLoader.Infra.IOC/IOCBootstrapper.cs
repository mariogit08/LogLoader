using Microsoft.Extensions.DependencyInjection;
using System;
using TReuters.LogLoader.Infra.IOC.Modules;

namespace TReuters.LogLoader.Infra.IOC
{
    public static class IOCBootstrapper
    {
        public static void RegisterInstances(this IServiceCollection services)
        {
            services.RegisterApplications();
            services.RegisterContexts();
            services.RegisterDomainServices();
            services.RegisterServices();
            services.RegisterRepositories();
            //services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory<SqlConnection>>();
            //services.AddTransient<IDbContext, DbContext>();
        }
    }
}
