using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
    public interface ISiteSettings
    {
        string SiteName { get; }
        string CallToActionHeader { get; }
        string CallToActionDescription { get; }
        IPublishedContent CallToActionUrl { get; }
        string CallToActionButtonLabel { get; }
        string FooterText { get; }
    }
}
