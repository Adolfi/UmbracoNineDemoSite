using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.ContentBlock
{
	[ViewComponent(Name = "contentBlock")]
	public class ContentBlockViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(BlockListItem model)
		{
			var block = model.Content as gM.ContentBlock;
			var viewModel = new ContentBlockViewModel()
			{
				Heading = block.Heading,
				BodyText = block.BodyText
			};
			return View(viewModel);
		}
	}
}
