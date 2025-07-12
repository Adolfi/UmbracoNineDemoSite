using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Models;
using UmbracoNineDemoSite.Core.Features.Search.Services;

namespace UmbracoNineDemoSite.Core.Features.Search.Controllers
{
    public class SearchApiController(SearchService searchService) : UmbracoApiController
    {
        public SearchResults? Search(string? searchTerm, int skip, int take)
        {
            var criteria = new BaseSearchCriteria { SearchTerm = searchTerm, Skip = skip, Take = take};
            var results = searchService.Search(criteria);

            return results;
        }
    }
}
