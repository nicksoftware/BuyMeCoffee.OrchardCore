using System;
using BuyMeCoffee.OrchardCore.Drivers;
using BuyMeCoffee.OrchardCore.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;

namespace BuyMeCoffee.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, BuyMeCoffeeSettingsDisplayDrivers>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(BuyMeCoffeeFilter));
            });
        }

    }
}