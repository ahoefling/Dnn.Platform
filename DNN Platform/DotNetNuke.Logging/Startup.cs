﻿using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetNuke.Logging
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, DnnLoggerFactory>();
            services.AddTransient<ILogger>(x => x.GetService<ILoggerFactory>().CreateLogger("DotNetNuke"));
        }
    }
}
