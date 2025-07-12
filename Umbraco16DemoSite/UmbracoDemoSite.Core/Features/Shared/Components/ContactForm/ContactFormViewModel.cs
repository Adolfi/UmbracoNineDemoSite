using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using generatedModels = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Core.Features.Shared.Components.ContactForm
{
    public class ContactFormViewModel(IBlockReference<IPublishedElement, IPublishedElement> item) : ContactFormModel
    {
        public string? Heading => item
            .Content
            .Value<string>(nameof(generatedModels.Page.Heading).ToFirstLower());        
    }
}