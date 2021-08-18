using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContentBlock
{
	public class ContentBlockViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(BlockListItem model)
		{
			return View(new ContentBlockViewModel(model));
		}
	}
}
