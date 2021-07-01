using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.Home
{
    public class HomeViewModel : SitePageBase, IHeadingPage
    {
        public HomeViewModel(IPublishedContent content) : base(content) {}

        public string Heading => this.Content.Value<string>("heading");
        public string Preamble => this.Content.Value<string>("preamble");
        public string BackgroundImage => this.Content.Value<string>("backgroundImage");
        public string CallToActionUrl => this.Content.Value<IPublishedContent>("callToActionUrl")?.Url();
        public string CallToActionLabel => this.Content.Value<string>("callToActionLabel");
    }
}
