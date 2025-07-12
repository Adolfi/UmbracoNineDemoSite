using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Shared;
using UmbracoNineDemoSite.Core.Features.Shared.Extensions;
using UmbracoNineDemoSite.Core.Models;
using UmbracoNineDemoSite.Core.Services;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
	public class SearchPageController(
        ILogger<SearchPageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IViewModelService viewModelService,
		IPublishedValueFallback publishedValueFallback) : PageBaseController(logger, compositeViewEngine, umbracoContextAccessor, viewModelService)
    {
        public IActionResult SearchPage(ContentModel model)
		{
            var mbModel = new generatedModels.SearchPage(model.Content, publishedValueFallback);
            if (mbModel == null)
            { return NotFound(); }

			var viewModel = new SearchPageViewModel()
			{
				Heading = mbModel.Heading,
				SearchForm = new SearchFormModel
				{
					NoResultsFound = mbModel.NoResultsFoundText,
					TotalResults = mbModel.TotalResults,
					SearchTermText = mbModel.SearchTermText
				}
			};
			
			MapSitePageBase(viewModel, mbModel);

			return CurrentTemplate(viewModel);
		}
	}
}
