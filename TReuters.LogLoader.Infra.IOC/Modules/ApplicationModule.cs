using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Application;
using TReuters.LogLoader.Application.Interfaces;

namespace TReuters.LogLoader.Infra.IOC.Modules
{
    public static class ApplicationModule
    {
        public static void RegisterApplications(this IServiceCollection services)
        {
            services.AddTransient<ILogAppService, LogAppService>();

        }
    }
}
