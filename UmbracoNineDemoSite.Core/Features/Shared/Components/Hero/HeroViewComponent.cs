using Microsoft.AspNetCore.Mvc;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Hero
{
	public class Hero : ViewComponent
	{
		public IViewComponentResult Invoke(HeroViewModel heroViewModel)
		{
			return View(heroViewModel);
		}
	}
}
