using System;
using Examine;
using Examine.Search;
using MailKit.Search;
using UmbracoNineDemoSite.Core.Features.Search.Criteria;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Search.Services
{
    public class SearchService
    {
        private readonly IExamineManager _examineManager;
        public SearchService(IExamineManager examineManager)
        {
            _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));
        }

        public ISearchResults Search(BaseSearchCriteria criteria)
        {
            if(!_examineManager.TryGetIndex(IndexNames.ExternalIndex, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name {IndexNames.ExternalIndex}");
            }

            var searchQuery = new Query.SearchQuery(index.Searcher);

            var query = searchQuery.BuildFilter(criteria);

            return query.Execute(new QueryOptions(0, 100));
        }
    }
}
