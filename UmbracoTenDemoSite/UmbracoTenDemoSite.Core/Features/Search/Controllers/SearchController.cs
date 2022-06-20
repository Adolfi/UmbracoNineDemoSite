using System.Linq;
using Examine;
using Examine.Lucene.Search;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;
using UmbracoTenDemoSite.Core.Features.Search.Models;
using UmbracoTenDemoSite.Core.Features.Search.Services;
using UmbracoTenDemoSite.Core.Features.SearchPage;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;

namespace UmbracoTenDemoSite.Core.Features.Search.Controllers
{
	public class SearchController : SurfaceController
	{
		private readonly SearchService _searchService;
		private readonly IPublishedUrlProvider _publishedUrlProvider;
		public SearchController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services, AppCaches appCaches,
			IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
			SearchService searchService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_searchService = searchService;
			_publishedUrlProvider = publishedUrlProvider;
		}

		public RedirectToUmbracoPageResult Search(SearchFormModel model)
		{
			var criteria = new BaseSearchCriteria()
			{
				SearchTerm = model.SearchTerm,
				Skip = model.Skip,
				Take = model.Take
			};

			var viewModel = new SearchResultViewModel()
			{
				Pages = FixContentUrl(_searchService.Search(criteria)),
				Products = _searchService.SearchProducts(criteria)
			};

			TempData.Add(TempDataKey.SearchResults, JsonConvert.SerializeObject(viewModel));
			return RedirectToCurrentUmbracoPage();
		}
		private SearchResults FixContentUrl(SearchResults results)
		{
			foreach (var result in results.Results)
			{
				if (!string.IsNullOrEmpty(result.Url)) continue;

				if (int.TryParse(result.Id, out int id))
				{
					result.Url = _publishedUrlProvider.GetUrl(id);
				}
			}
			return results;
		}
	}
}
