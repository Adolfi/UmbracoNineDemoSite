using UmbracoNineDemoSite.Core.Features.Search.Models;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
    public class SearchFormModel
    {
        public string SearchTerm { get; set; }
        public string TotalResults { get; set; }
        public string NoResultsFound { get; set; }
        public string SearchTermText { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public SearchResults SearchResults { get; set; }
    }
}
