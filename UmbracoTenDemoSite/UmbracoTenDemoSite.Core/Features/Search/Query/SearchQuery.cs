using Examine;
using Examine.Search;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;
using UmbracoTenDemoSite.Core.Features.Search.Query.Filters;
using generatedModels = UmbracoTenDemoSite.Core;

namespace UmbracoTenDemoSite.Core.Features.Search.Query
{
    public class SearchQuery : BaseSearchQuery<BaseSearchCriteria>
    {
        public SearchQuery(ISearcher searcher) : base(searcher)
        {
        }

        public override IBooleanOperation BuildFilter(BaseSearchCriteria searchCriteria)
        {
            var query = _searcher.CreateQuery("content");

            var filter = query.FilterByAlias(new[] { generatedModels.Page.ModelTypeAlias, generatedModels.Home.ModelTypeAlias})
                .SearchByTerm(searchCriteria.SearchTerm);

            return filter;
        }
    }
}
