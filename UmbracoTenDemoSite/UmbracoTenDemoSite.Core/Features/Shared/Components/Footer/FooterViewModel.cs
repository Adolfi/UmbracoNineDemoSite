namespace UmbracoTenDemoSite.Core.Features.Shared.Components.Footer
{
    public class FooterViewModel
    {
        public string CallToActionHeader { get; set; }
        public string CallToActionDescription { get; set; }
        public string CallToActionUrl { get; set; }
        public string CallToActionButtonLabel { get; set; }
        public bool HasCallToAction => !string.IsNullOrEmpty(this.CallToActionHeader) && !string.IsNullOrEmpty(this.CallToActionDescription);
        public bool HasCallToActionButton => !string.IsNullOrEmpty(this.CallToActionUrl) && !string.IsNullOrEmpty(this.CallToActionButtonLabel);
        public string Text { get; set; }
    }
}
