using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Header
{
    [ViewComponent(Name = "Header")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISiteSettings siteSettings;
        private readonly UmbracoHelper umbracoHelper;

        public HeaderViewComponent(ISiteSettings siteSettings, UmbracoHelper umbracoHelper)
        {
            this.siteSettings = siteSettings;
            this.umbracoHelper = umbracoHelper;
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
