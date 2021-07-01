using Microsoft.AspNetCore.Mvc;
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
                Text = this.siteSettings.FooterText
            });
        }
    }
}
