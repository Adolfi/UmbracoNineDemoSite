using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Extensions;
using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
	public class SearchPageController : RenderController
	{
		public SearchPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) { }

		public IActionResult SearchPage(ContentModel model)
		{
			var mbModel = model.Content as gM.SearchPage ?? new gM.SearchPage(model.Content, null);
			var viewModel = new SearchPageViewModel()
			{
				//SiteName = mbModel?.Root()?.Name,
				//Id = mbModel.Id,
				//Name = mbModel.Name,
				//PageTitle = mbModel.PageTitle ?? mbModel.Name,
				//PageDescription = mbModel.PageDescription,

				Heading = mbModel.Heading,
				SearchForm = new SearchFormModel
				{
					NoResultsFound = mbModel.NoResultsFoundText,
					TotalResults = mbModel.TotalResults,
					SearchTermText = mbModel.SearchTermText
				}
			};
			viewModel.MapSitePageBase(mbModel);

			return CurrentTemplate(viewModel);
		}
	}
}
