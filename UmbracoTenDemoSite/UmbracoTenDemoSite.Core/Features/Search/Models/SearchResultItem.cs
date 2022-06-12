using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Search.Models
{
    public class SearchResultItem
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }
}
