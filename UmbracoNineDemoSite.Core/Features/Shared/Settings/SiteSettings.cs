using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public class SiteSettings : ISiteSettings
    {
        private readonly UmbracoHelper umbracoHelper;

        public SiteSettings(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
        }

        private IPublishedContent home => this.umbracoHelper.ContentAtXPath($"//{ContentTypeAlias.Home}")?.FirstOrDefault();
        private IPublishedContent settings => this.umbracoHelper.ContentAtXPath($"//{ContentTypeAlias.SiteSettings}")?.FirstOrDefault();

        public string SiteName => this.home.Name;
        public string CallToActionHeader => this.settings.Value<string>(PropertyAlias.CallToActionHeader);
        public string CallToActionDescription => this.settings.Value<string>(PropertyAlias.CallToActionDescription);
        public IPublishedContent CallToActionUrl => this.settings.Value<IPublishedContent>(PropertyAlias.CallToActionUrl);
        public string CallToActionButtonLabel => this.settings.Value<string>(PropertyAlias.CallToActionButtonLabel);
        public string FooterText => this.settings.Value<string>(PropertyAlias.FooterText);
    }
}
