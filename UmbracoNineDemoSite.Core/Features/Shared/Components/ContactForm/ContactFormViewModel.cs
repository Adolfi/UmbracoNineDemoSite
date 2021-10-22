using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm
{
    public class ContactFormViewModel : ContactForm
    {
        private readonly BlockListItem block;

        public ContactFormViewModel(BlockListItem block)
        {
            this.block = block;
        }

        public string Heading => this.block.Content.Value<string>(nameof(generatedModels.Page.Heading).ToFirstLower());        
    }
}
