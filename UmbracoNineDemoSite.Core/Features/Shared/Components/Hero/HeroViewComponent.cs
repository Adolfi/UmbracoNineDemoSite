using Microsoft.AspNetCore.Mvc;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Hero
{
	public class HeroViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(HeroViewModel heroViewModel)
		{
			return View(heroViewModel);
		}
	}
}
