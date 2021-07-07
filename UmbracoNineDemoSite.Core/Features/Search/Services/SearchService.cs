using System;
using System.Linq;
using Examine;
using Examine.Search;
using MailKit.Search;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Search.Models;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using SearchResults = UmbracoNineDemoSite.Core.Features.Search.Models.SearchResults;

namespace UmbracoNineDemoSite.Core.Features.Search.Services
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
                    Description = x.Values[SearchField.BodyText]

                })?.ToList()
            };

            return results;
        }
    }
}
