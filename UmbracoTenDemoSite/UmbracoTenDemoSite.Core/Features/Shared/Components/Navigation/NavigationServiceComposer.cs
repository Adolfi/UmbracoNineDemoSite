using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoTenDemoSite.Core.Features.Shared.Components.Navigation;

namespace UmbracoTenDemoSite.Core.Features.Shared.Settings
{
    public class NavigationServiceComposer : IComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<INavigationService, NavigationService>();
        }
    }
}
