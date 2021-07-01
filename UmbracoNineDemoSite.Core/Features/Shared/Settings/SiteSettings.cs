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

        private IPublishedContent settings => this.umbracoHelper.ContentAtXPath($"//{ContentTypeAlias.SiteSettings}")?.FirstOrDefault();

        public string SiteName => this.settings.Value<string>("siteName");
        public string CallToActionHeader => this.settings.Value<string>("callToActionHeader");
        public string CallToActionDescription => this.settings.Value<string>("callToActionDescription");
        public IPublishedContent CallToActionUrl => this.settings.Value<IPublishedContent>("callToActionUrl");
        public string CallToActionButtonLabel => this.settings.Value<string>("callToActionButtonLabel");
        public string FooterText => this.settings.Value<string>("footerText");
    }
}
