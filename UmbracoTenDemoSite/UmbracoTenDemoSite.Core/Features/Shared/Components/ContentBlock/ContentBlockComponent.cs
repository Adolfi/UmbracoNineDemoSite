using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using generatedModels = UmbracoTenDemoSite.Core;

namespace UmbracoTenDemoSite.Core.Features.Shared.Components.ContentBlock
{
	[ViewComponent(Name = "contentBlock")]
	public class ContentBlockViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(BlockListItem model)
		{
			var block = model.Content as generatedModels.ContentBlock;
			var viewModel = new ContentBlockViewModel()
			{
				Heading = block.Heading,
				BodyText = block.BodyText
			};
			return View(viewModel);
		}
	}
}
