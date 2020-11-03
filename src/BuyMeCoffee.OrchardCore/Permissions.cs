using OrchardCore.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeCoffee.OrchardCore
{
 public   class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageBuyMeCoffee = new Permission("Buy Me a Coffee", "Manage Buy Me a Coffee");


        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageBuyMeCoffee }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                   Name = "Administrator",
                    Permissions = new []{ ManageBuyMeCoffee },
                }
            };
        }

    }
}
