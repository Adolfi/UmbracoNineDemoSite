using Microsoft.AspNetCore.Mvc;
using UmbracoDemoSite.Core.Features.Shared.Settings;

namespace UmbracoDemoSite.Core.Features.Shared.Components.Header
{
    [ViewComponent(Name = "Header")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISiteSettingsViewModel siteSettings;

        public HeaderViewComponent(ISiteSettingsViewModel siteSettings)
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
