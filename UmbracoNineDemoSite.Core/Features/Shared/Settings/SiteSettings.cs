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

        private IPublishedContent home => this.umbracoHelper.ContentAtRoot().FirstOrDefault(x => x.ContentType.Alias == ContentTypeAlias.Home);
        private IPublishedContent settings => home?.FirstChild(x => x.ContentType.Alias == ContentTypeAlias.SiteSettings);

        public string SiteName => this.home.Name;
        public string CallToActionHeader => this.settings.GetProperty(PropertyAlias.CallToActionHeader).GetValue() as string;
        public string CallToActionDescription => this.settings.GetProperty(PropertyAlias.CallToActionDescription).GetValue() as string;
        public IPublishedContent CallToActionUrl => this.settings.GetProperty(PropertyAlias.CallToActionUrl).GetValue() as IPublishedContent;
        public string CallToActionButtonLabel => this.settings.GetProperty(PropertyAlias.CallToActionButtonLabel).GetValue() as string;
        public string FooterText => this.settings.GetProperty(PropertyAlias.FooterText).GetValue() as string;
    }
}
