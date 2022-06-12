namespace UmbracoTenDemoSite.Core.Features.Shared.Components.Hero
{
    public class HeroViewModel
    {
        public string Heading { get; set; }
        public string Preamble { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string CallToActionUrl { get; set; }
        public string CallToActionLabel { get; set; }
        public bool HasCallToAction => !string.IsNullOrEmpty(this.CallToActionUrl) && !string.IsNullOrEmpty(this.CallToActionLabel);
    }
}
