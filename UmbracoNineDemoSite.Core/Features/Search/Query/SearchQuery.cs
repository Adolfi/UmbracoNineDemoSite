using Examine;
using Examine.Search;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Query.Filters;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Search.Query
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
