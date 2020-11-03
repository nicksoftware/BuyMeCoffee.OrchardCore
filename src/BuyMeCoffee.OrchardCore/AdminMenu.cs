using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeCoffee.OrchardCore
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer<AdminMenu> T;

        public AdminMenu(IStringLocalizer<AdminMenu> t)
        {
            T = t;
        }
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if(!string.Equals(name, "admin",StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(T["Configuration"], config => config
            .Add(T["Settings"], set => set
                .Add(T["Payments"],pa => pa
                   .Add(T["Buy Me a Coffee"], T["Buy Me a Coffee"].PrefixPosition(), se => se
                     .AddClass("buyMeCoffee").Id("buyMeCoffee")
                         .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.GroupId })
                         .LocalNav()
            ))));

            return Task.CompletedTask;
        }
    }
}
