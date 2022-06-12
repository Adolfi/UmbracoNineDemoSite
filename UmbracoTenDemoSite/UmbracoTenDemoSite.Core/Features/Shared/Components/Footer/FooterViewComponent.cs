using Microsoft.AspNetCore.Mvc;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Footer
{
    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISiteSettings siteSettings;

        public FooterViewComponent(ISiteSettings siteSettings)
        {
            this.siteSettings = siteSettings;
        }

        public IViewComponentResult Invoke()
        {
            return View(new FooterViewModel()
            {
                CallToActionHeader = this.siteSettings.CallToActionHeader,
                CallToActionDescription = this.siteSettings.CallToActionDescription,
                CallToActionUrl = this.siteSettings.CallToActionUrl?.Url(),
                CallToActionButtonLabel = this.siteSettings.CallToActionButtonLabel,
                Text = this.siteSettings.FooterText
            });
        }
    }
}
