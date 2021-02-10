using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Domain.Interfaces.Repositories;
using TReuters.LogLoader.Infra.Data.Repositories;
using TReuters.LogLoader.Infra.Data.UnitOfWork;

namespace TReuters.LogLoader.Infra.IOC.Modules
{
    public static class RepositoryModule
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory<NpgsqlConnection>>();            
        }
    }
}
