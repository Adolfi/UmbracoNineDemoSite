using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
    public class SitePageBase : ContentModel
    {
        public SitePageBase(IPublishedContent content) : base(content) { }

        public string BodyClass = "frontpage theme-font-serif theme-color-earth";

        public int Id => this.Content.Id;

        public string Name => this.Content.Name;        
    }
}
