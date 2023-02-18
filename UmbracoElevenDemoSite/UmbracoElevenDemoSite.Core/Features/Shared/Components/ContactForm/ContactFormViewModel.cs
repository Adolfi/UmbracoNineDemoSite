using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;
using generatedModels = UmbracoElevenDemoSite.Core;

namespace UmbracoElevenDemoSite.Core.Features.Shared.Components.ContactForm
{
    public class ContactFormViewModel : ContactForm
    {
        private readonly BlockListItem block;

        public ContactFormViewModel(BlockListItem block)
        {
            this.block = block;
            this.Subject = block.Settings.Value<string>(nameof(ContactFormSettings.Subject).ToFirstLower());
        }

        public string Heading => this.block.Content.Value<string>(nameof(generatedModels.Page.Heading).ToFirstLower());        
    }
}
