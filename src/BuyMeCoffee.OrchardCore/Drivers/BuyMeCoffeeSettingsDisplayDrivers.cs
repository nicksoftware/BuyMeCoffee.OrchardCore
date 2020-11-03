using BuyMeCoffee.OrchardCore.Settings;
using BuyMeCoffee.OrchardCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeCoffee.OrchardCore.Drivers
{
    class BuyMeCoffeeSettingsDisplayDrivers : SectionDisplayDriver<ISite, BuyMeCoffeeSettings>
    {
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BuyMeCoffeeSettingsDisplayDrivers(
            IShellHost shellHost,
            ShellSettings shellSettings,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _shellHost = shellHost;
            _shellSettings = shellSettings;
            _authService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async override Task<IDisplayResult> EditAsync(BuyMeCoffeeSettings section, BuildEditorContext context)
        {
            if (!await IsUserAllowedToManage()) return null ;

            return Initialize<BuyMeCoffeeViewModel>("BuyMeCoffeeSettings_Edit", model =>
            {
                model.DataId = section.DataId?.ToString() ?? string.Empty;
                model.Description = section.Description?.ToString() ?? string.Empty;
                model.Message = section.Message?.ToString() ?? string.Empty;
                model.WidgetColor = section.WidgetColor?.ToString() ?? string.Empty;
                model.WidgetPosition = section.WidgetPosition?.ToString() ?? string.Empty;
                model.XMargin = section.XMargin;
                model.YMargin = section.YMargin;
                model.WidgetVersion = section.WidgetVersion;

            }).Location("Content").OnGroup(Constants.GroupId);
        }

        private async Task<bool> IsUserAllowedToManage()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return await _authService.AuthorizeAsync(user, Permissions.ManageBuyMeCoffee);
        }

        public async override Task<IDisplayResult> UpdateAsync(BuyMeCoffeeSettings section,  BuildEditorContext context)
        {
            if (!await IsUserAllowedToManage()) return null;

            if(Constants.GroupId == context.GroupId)
            {
                var model = new BuyMeCoffeeViewModel();
                
                if(await context.Updater.TryUpdateModelAsync(model, Prefix))
                {
                    section.DataId =        model.DataId?.Trim() ;
                    section.Description =   model.Description?.Trim();
                    section.Message =       model.Message?.Trim();
                    section.WidgetColor =   model.WidgetColor?.Trim();
                    section.WidgetPosition= model.WidgetPosition?.Trim();
                    section.XMargin =       model.XMargin;
                    section.YMargin =       model.YMargin;
                    section.WidgetVersion = model.WidgetVersion?.Trim();
                }
            }

            return await EditAsync(section, context);
        }
    }
}
