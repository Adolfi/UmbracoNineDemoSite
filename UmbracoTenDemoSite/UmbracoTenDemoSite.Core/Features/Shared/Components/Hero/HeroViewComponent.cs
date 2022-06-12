using Microsoft.AspNetCore.Mvc;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Hero
{
	[ViewComponent(Name = "Hero")]
	public class HeroComponent : ViewComponent
	{
		public IViewComponentResult Invoke(HeroViewModel heroViewModel)
		{
			return View(heroViewModel);
		}
	}
}
