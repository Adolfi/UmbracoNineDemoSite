using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class NavigationServiceComposer : IComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<INavigationService, NavigationService>();
        }
    }
}
