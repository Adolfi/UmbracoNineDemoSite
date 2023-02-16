using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoElevenDemoSite.Core.Features.Shared.Components.Navigation;

namespace UmbracoElevenDemoSite.Core.Features.Shared.Settings
{
    public class NavigationServiceComposer : IComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<INavigationService, NavigationService>();
        }
    }
}
