using Examine;
using Umbraco.Cms.Infrastructure.Search;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;
using UmbracoTenDemoSite.Core.Features.Search.Models;
using UmbracoTenDemoSite.Core.Features.Search.Services;

namespace UmbracoTenDemoSite.Core.Features.Search.Controllers
{
    public class SearchApiController : UmbracoApiController
    {
        private readonly SearchService _searchService;
        public SearchApiController(SearchService searchService)
        {
            _searchService = searchService;
        }
        public SearchResults Search(string searchTerm, int skip, int take)
        {
            var criteria = new BaseSearchCriteria { SearchTerm = searchTerm, Skip = skip, Take = take};
            var results = _searchService.Search(criteria);

            return results;
        }
    }
}
