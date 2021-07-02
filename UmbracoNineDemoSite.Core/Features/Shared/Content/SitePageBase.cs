using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
    public class SitePageBase : ContentModel
    {
        public SitePageBase(IPublishedContent content) : base(content) { }

        public string BodyClass = "frontpage theme-font-serif theme-color-earth";

        public int Id => this.Content.Id;

        public string Name => this.Content.Name;
        
        public string PageTitle => this.Content.Value<string>(PropertyAlias.PageTitle);

        public string SiteName => this.Content.AncestorOrSelf(1).Name;
    }
}
