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
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Models;
using UmbracoNineDemoSite.Core.Features.Search.Services;
using UmbracoNineDemoSite.Core.Features.SearchPage;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Controllers
{
    public class SearchController : SurfaceController
    {
        private readonly SearchService _searchService;
        public SearchController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services, AppCaches appCaches,
            IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
            SearchService searchService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _searchService = searchService;
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
                Pages = _searchService.Search(criteria),
                Products = _searchService.SearchProducts(criteria)
            };

            TempData.Add(TempDataKey.SearchResults, JsonConvert.SerializeObject(viewModel));
            return RedirectToCurrentUmbracoPage();
        }
    }
}
