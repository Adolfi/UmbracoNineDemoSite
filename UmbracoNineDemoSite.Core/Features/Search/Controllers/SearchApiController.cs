using Examine;
using Umbraco.Cms.Infrastructure.Search;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Services;

namespace UmbracoNineDemoSite.Core.Features.Search.Controllers
{
    public class SearchApiController : UmbracoApiController
    {
        private readonly SearchService _searchService;
        public SearchApiController(SearchService searchService)
        {
            _searchService = searchService;
        }
        public ISearchResults Search(string searchTerm, int skip, int take)
        {
            var criteria = new BaseSearchCriteria { SearchTerm = searchTerm, Skip = skip, Take = take};
            var results = _searchService.Search(criteria);

            return results;
        }
    }
}
