using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;
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

        public string Heading => this.block.Content.Value<string>(PropertyAlias.Heading);

        public string BodyText => this.block.Content.Value<string>(PropertyAlias.BodyText);
    }
}
