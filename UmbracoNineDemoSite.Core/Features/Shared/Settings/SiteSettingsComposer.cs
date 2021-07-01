using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class SiteSettingsComposer : IUserComposer
	{
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<ISiteSettings, SiteSettings>();
        }
    }
}
