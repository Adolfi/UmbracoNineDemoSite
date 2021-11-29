using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class SiteSettingsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //per page request SiteSettings are called twice (Header and Footer components)
            //therefore scoped is more efficient then transient
            builder.Services.AddScoped<ISiteSettings, SiteSettings>();
        }
    }
}
