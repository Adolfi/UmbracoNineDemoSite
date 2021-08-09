using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.Page
{
    public class PageViewModel : SitePageBase, IHeadingPage
    {
        public PageViewModel(IPublishedContent content) : base(content) { }

        public string Heading => this.Content.GetProperty(PropertyAlias.Heading).GetValue() as string;

        public string BodyText => this.Content.GetProperty(PropertyAlias.BodyText).GetValue() as string;

        public BlockListModel Blocks => this.Content.GetProperty(PropertyAlias.Blocks).GetValue() as BlockListModel;
    }
}
