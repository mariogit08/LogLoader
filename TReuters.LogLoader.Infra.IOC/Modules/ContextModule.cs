using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Domain.Interfaces.DBContext;
using TReuters.LogLoader.Infra.Data;
using TReuters.LogLoader.Infra.Data.UnitOfWork;

namespace TReuters.LogLoader.Infra.IOC.Modules
{
    public static class ContextModule
    {
        public static void RegisterContexts(this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionFactory, PostgreConnectionFactory>();
            services.AddTransient<IDBContext, DBContext>();            
        }
    }
}
