using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Extensions;
using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Home
{
	public class HomeController : RenderController
	{
		public HomeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) { }

		public IActionResult Home(ContentModel model)
		{
			var mbModel = model.Content as gM.Home ?? new gM.Home(model.Content, null);
			var viewModel = new HomeViewModel()
			{
				Heading = mbModel.Heading,
				Preamble = mbModel.Preamble,
				BackgroundImage = mbModel.BackgroundImage,
				CallToActionLabel = mbModel.CallToActionLabel,
				CallToActionUrl = mbModel.CallToActionUrl?.Url(),
				Blocks = mbModel.Blocks
			};
			viewModel.Hero = new Shared.Components.Hero.HeroViewModel()
			{
				CallToActionUrl = viewModel.CallToActionUrl,
				CallToActionLabel = viewModel.CallToActionLabel,
				BackgroundImageUrl = viewModel.BackgroundImage,
				Preamble = viewModel.Preamble,
				Heading = viewModel.Heading
			};
			viewModel.MapSitePageBase(mbModel);

			return View(viewModel);
		}
	}
}