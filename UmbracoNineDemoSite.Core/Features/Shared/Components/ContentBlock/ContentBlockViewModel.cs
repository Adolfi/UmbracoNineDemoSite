using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContentBlock
{
    public class ContentBlockViewModel
    {
        private readonly BlockListItem block;

        public ContentBlockViewModel(BlockListItem block)
        {
            this.block = block;
        }

        public string Heading => this.block.Content.GetProperty(PropertyAlias.Heading).GetValue() as string;

        public HtmlEncodedString BodyText => this.block.Content.GetProperty(PropertyAlias.BodyText).GetValue() as HtmlEncodedString;
    }
}
