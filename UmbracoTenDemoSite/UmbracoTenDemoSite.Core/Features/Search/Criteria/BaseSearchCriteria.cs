namespace UmbracoTenDemoSite.Core.Features.Search.Criteria
{
    public class BaseSearchCriteria
    {
        public string SearchTerm { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
