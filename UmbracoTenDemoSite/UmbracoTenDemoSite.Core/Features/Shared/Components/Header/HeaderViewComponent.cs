using Microsoft.AspNetCore.Mvc;
using UmbracoTenDemoSite.Core.Features.Shared.Settings;

namespace UmbracoTenDemoSite.Core.Features.Shared.Components.Header
{
    [ViewComponent(Name = "Header")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISiteSettings siteSettings;

        public HeaderViewComponent(ISiteSettings siteSettings)
        {
            this.siteSettings = siteSettings;
        }

        public IViewComponentResult Invoke(int selected)
        {
            return View(new HeaderViewModel()
            {
                Heading = this.siteSettings.SiteName,
                Selected = selected
            });
        }
    }
}
