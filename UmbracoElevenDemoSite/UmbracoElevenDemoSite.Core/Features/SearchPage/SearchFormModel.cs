using UmbracoElevenDemoSite.Core.Features.Search.Models;

namespace UmbracoElevenDemoSite.Core.Features.SearchPage
{
    public class SearchFormModel
    {
        public string SearchTerm { get; set; }
        public string TotalResults { get; set; }
        public string NoResultsFound { get; set; }
        public string SearchTermText { get; set; }
        public int Skip => 0; // This is here for future improvment with pagination.
        public int Take => 100; // This is here for future improvment with pagination.
        public SearchResults SearchResults { get; set; }
    }
}
