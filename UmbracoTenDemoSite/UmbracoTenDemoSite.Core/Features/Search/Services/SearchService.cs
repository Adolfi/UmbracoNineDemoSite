using System;
using System.Linq;
using Examine;
using Examine.Search;
using MailKit.Search;
using Umbraco.Cms.Web.Common;
using UmbracoTenDemoSite.Core.Features.Search.Criteria;
using UmbracoTenDemoSite.Core.Features.Search.Models;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using SearchResults = UmbracoTenDemoSite.Core.Features.Search.Models.SearchResults;

namespace UmbracoTenDemoSite.Core.Features.Search.Services
{
    public class SearchService
    {
        private readonly IExamineManager _examineManager;
        public SearchService(IExamineManager examineManager)
        {
            _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));
        }

        public SearchResults Search(BaseSearchCriteria criteria)
        {
            if (!_examineManager.TryGetIndex(IndexNames.ExternalIndex, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name {IndexNames.ExternalIndex}");
            }

            var searchQuery = new Query.SearchQuery(index.Searcher);

            var query = searchQuery.BuildFilter(criteria);

            var searchResults = query.Execute(new QueryOptions(criteria.Skip, criteria.Take));

            return GetSearchResults(searchResults, criteria.SearchTerm);
        }

        public SearchResults SearchProducts(BaseSearchCriteria criteria)
        {
            if (!_examineManager.TryGetIndex(IndexNames.ProductsIndex, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name {IndexNames.ProductsIndex}");
            }

            var searchQuery = new Query.ProductSearchQuery(index.Searcher);

            var query = searchQuery.BuildFilter(criteria);

            var searchResults = query.Execute(new QueryOptions(criteria.Skip, criteria.Take));

            return GetProductsSearchResults(searchResults, criteria.SearchTerm);
        }
         private SearchResults GetProductsSearchResults(ISearchResults searchResults, string searchTerm)
        {
            if (searchResults == null) return null;

            var results = new SearchResults
            {
                SearchTerm = searchTerm,
                TotalCount = searchResults.TotalItemCount,
                Results = searchResults?.Select(x => new SearchResultItem()
                {
                    Heading = x.Values[SearchField.Name],
                    Description = x.Values[SearchField.Description],
                    Url = x.Values["url"]

                })?.ToList()
            };

            return results;
        }

        private SearchResults GetSearchResults(ISearchResults searchResults, string searchTerm)
        {
            if (searchResults == null) return null;

            var results = new SearchResults
            {
                SearchTerm = searchTerm,
                TotalCount = searchResults.TotalItemCount,
                Results = searchResults?.Select(x => new SearchResultItem()
                {
                    Heading = x.Values[SearchField.Heading],
                    Description = x.Values[SearchField.BodyText],
                    Id = x.Id

                })?.ToList()
            };

            return results;
        }
    }
}
