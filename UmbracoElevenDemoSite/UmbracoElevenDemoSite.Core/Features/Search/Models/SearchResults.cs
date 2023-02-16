using System.Collections.Generic;

namespace UmbracoElevenDemoSite.Core.Features.Search.Models
{
    public class SearchResults
    {
        public long TotalCount { get; set; }
        public string SearchTerm { get; set; }
        public IList<SearchResultItem> Results { get; set; }
    }
}
