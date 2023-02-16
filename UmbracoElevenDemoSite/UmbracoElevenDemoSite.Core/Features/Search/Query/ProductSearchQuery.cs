using Examine;
using Examine.Search;
using Umbraco.Extensions;
using UmbracoElevenDemoSite.Core.Features.Search.Criteria;
using UmbracoElevenDemoSite.Core.Features.Search.Query.Filters;
using UmbracoElevenDemoSite.Core.Features.Shared.Constants;
using UmbracoElevenDemoSite.Integrations.Products.Entities;

namespace UmbracoElevenDemoSite.Core.Features.Search.Query
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
