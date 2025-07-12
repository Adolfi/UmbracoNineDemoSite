using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoNineDemoSite.Core.Features.Shared.Variables
{
    public class SiteVariableComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<ISiteVariable, SiteVariableService>();
        }
    }
}
