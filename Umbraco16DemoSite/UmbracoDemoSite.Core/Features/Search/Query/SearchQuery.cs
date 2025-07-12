using Examine;
using Examine.Search;
using UmbracoDemoSite.Core.Features.Search.Criteria;
using UmbracoDemoSite.Core.Features.Search.Query.Filters;
using generatedModels = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Core.Features.Search.Query
{
    public class SearchQuery(ISearcher searcher) : BaseSearchQuery<BaseSearchCriteria>(searcher)
    {
        public override IBooleanOperation? BuildFilter(BaseSearchCriteria searchCriteria)
        {
            var query = _searcher?.CreateQuery("content");

            var filter = query?
                .FilterByAlias(
                    [generatedModels.Page.ModelTypeAlias, generatedModels.Home.ModelTypeAlias])
                .SearchByTerm(searchCriteria.SearchTerm ?? string.Empty);

            return filter;
        }
    }
}
