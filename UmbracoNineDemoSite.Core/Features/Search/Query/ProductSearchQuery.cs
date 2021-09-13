using Examine;
using Examine.Search;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Query.Filters;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Query
{
    public class ProductSearchQuery : BaseSearchQuery<BaseSearchCriteria>
    {
        private readonly ISearcher _searcher;
        public ProductSearchQuery(ISearcher searcher) : base(searcher)
        {
            _searcher = searcher;
        }

        public override IBooleanOperation BuildFilter(BaseSearchCriteria searchCriteria)
        {
            var query = _searcher.CreateQuery("content");

            var filter = query.FilterByAlias(new[] { ContentTypeAlias.ExternalProduct })
                .SearchByTerm(searchCriteria.SearchTerm);

            return filter;
        }
    }
}
