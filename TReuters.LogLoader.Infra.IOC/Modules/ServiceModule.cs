using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Infra.Service;
using TReuters.LogLoader.Infra.Service.Interfaces;

namespace TReuters.LogLoader.Infra.IOC.Modules
{
    public static class ServiceModule
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IBatchFileLogExtractorService, BatchFileLogExtractorService>();
        }
    }
}
