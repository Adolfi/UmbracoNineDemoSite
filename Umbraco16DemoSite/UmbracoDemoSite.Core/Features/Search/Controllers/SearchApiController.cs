using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemoSite.Core.Features.Search.Criteria;
using UmbracoDemoSite.Core.Features.Search.Models;
using UmbracoDemoSite.Core.Features.Search.Services;

namespace UmbracoDemoSite.Core.Features.Search.Controllers
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
