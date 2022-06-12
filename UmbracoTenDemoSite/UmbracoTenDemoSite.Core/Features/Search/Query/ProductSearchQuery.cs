using Examine;
using Examine.Search;
using Umbraco.Extensions;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;
using UmbracoTenDemoSite.Core.Features.Search.Query.Filters;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using UmbracoTenDemoSite.Integrations.Products.Entities;

namespace UmbracoTenDemoSite.Core.Features.Search.Query
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

            var filter = query.FilterByAlias(new[] { nameof(Product).ToFirstLower() })
                .SearchByTerm(searchCriteria.SearchTerm);

            return filter;
        }
    }
}
