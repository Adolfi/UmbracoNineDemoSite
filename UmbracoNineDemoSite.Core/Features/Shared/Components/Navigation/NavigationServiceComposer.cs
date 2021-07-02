using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class NavigationServiceComposer : IUserComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<INavigationService, NavigationService>();
        }
    }
}
