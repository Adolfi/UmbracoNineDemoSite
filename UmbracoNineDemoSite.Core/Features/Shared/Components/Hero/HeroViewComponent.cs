using Microsoft.AspNetCore.Mvc;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Hero
{
    [ViewComponent(Name = "Hero")]
    public class HeroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string heading, string preamble, string backgroundImageUrl, string callToActionUrl, string callToActionLabel)
        {
            return View(new HeroViewModel()
            {
                Heading = heading,
                Preamble = preamble,
                BackgroundImageUrl = backgroundImageUrl,
                CallToActionUrl = callToActionUrl,
                CallToActionLabel = callToActionLabel
            });
        }
    }
}
