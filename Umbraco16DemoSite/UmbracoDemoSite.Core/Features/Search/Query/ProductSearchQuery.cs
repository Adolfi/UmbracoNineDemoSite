using Examine;
using Examine.Search;
using Umbraco.Extensions;
using UmbracoDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Query.Filters;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Query
{
    public class ProductSearchQuery(ISearcher searcher) : BaseSearchQuery<BaseSearchCriteria>(searcher)
    {
        public override IBooleanOperation? BuildFilter(BaseSearchCriteria searchCriteria)
        {
            var query = _searcher?.CreateQuery("content");

            var filter = query?.FilterByAlias([nameof(Product).ToFirstLower()])
                .SearchByTerm(searchCriteria.SearchTerm ?? string.Empty);

            return filter;
        }
    }
}
