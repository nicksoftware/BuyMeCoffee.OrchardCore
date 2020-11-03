using BuyMeCoffee.OrchardCore.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace BuyMeCoffee.OrchardCore.Filters
{
   public  class BuyMeCoffeeFilter : IAsyncResultFilter
    {
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        public BuyMeCoffeeFilter(
            IResourceManager resourceManager,
            ISiteService siteService
            )
        {
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, 
            ResultExecutionDelegate next)
        {
           //Only inject in client 
            if (!AdminAttribute.IsApplied(context.HttpContext) && (context.Result is ViewResult ||
                context.Result is PageResult))
            {
                var settings = (await _siteService.GetSiteSettingsAsync()).As<BuyMeCoffeeSettings>();

                if (!string.IsNullOrEmpty(settings.DataId))
                {
                    // build the script
                    var script = ScriptBuilder(settings);

                    _resourceManager.RegisterHeadScript(new HtmlString(script));
                }
            }

            await next.Invoke();
        }

     
        public string ScriptBuilder(BuyMeCoffeeSettings settings)
        {

            StringBuilder scriptBuilder = new StringBuilder();

            scriptBuilder
                .Append("<script data-name = \"BMC-Widget\" ")
                .Append("src =\"" + Constants.BuyMECoffeeUrl + "/" + settings.WidgetVersion + "/widget.prod.min.js\" ")
                .Append("data-id =\"" + settings.DataId + "\" ")
                .Append("data-description=\"" + settings.Description + "\" ")
                .Append("data-message=\"" + settings.Message + "\" ")
                .Append("data-color=\"" + settings.WidgetColor + "\" ")
                .Append("data-position=\"" + settings.WidgetPosition + "\" ")
                .Append("data-x_margin=\"" + settings.XMargin + "\" ")
                .Append("data-y_margin=\"" + settings.YMargin + "\" ")
                .AppendLine("></script >");
            return scriptBuilder.ToString();
        }
    }
}
