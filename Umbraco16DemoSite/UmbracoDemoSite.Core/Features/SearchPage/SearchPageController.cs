using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemoSite.Core.Features.Shared;
using UmbracoDemoSite.Core.Features.Shared.Extensions;
using UmbracoDemoSite.Core.Models;
using UmbracoDemoSite.Core.Services;
using generatedModels = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Core.Features.SearchPage
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
