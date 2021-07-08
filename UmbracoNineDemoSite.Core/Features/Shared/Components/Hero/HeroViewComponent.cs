using Microsoft.AspNetCore.Mvc;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Hero
{
	//The following two changes simplify the code according to https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0
	// A view component class can be created by any of the following:
	//	- Deriving from ViewComponent
	//	- Decorating a class with the[ViewComponent] attribute, or deriving from a class with the[ViewComponent] attribute
	//	- Creating a class where the name ends with the suffix ViewComponent

	//[ViewComponent(Name = "Hero")]
	//public class HeroComponent : ViewComponent
	public class Hero : ViewComponent
	{
		public IViewComponentResult Invoke(HeroViewModel heroViewModel)
		{
			return View(heroViewModel);
		}
	}
}
