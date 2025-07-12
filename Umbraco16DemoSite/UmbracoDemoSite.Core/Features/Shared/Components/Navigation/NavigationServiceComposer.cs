using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoDemoSite.Core.Features.Shared.Components.Navigation
{
    public class NavigationServiceComposer : IComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<INavigationService, NavigationService>();
        }
    }
}
