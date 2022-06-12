using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoNineDemoSite.Core.Features.Shared.Variables;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class SiteVariableComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<ISiteVariable, SiteVariableService>();
        }
    }
}
