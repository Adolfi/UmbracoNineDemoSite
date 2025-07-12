using System;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Tests.Models
{
    public class ContentDetails
    {
        public ContentDetails() { }
        public ContentDetails(int id, Guid key, string name, string urlSegment, string contentTypeAlias, int level = 0, Guid? parentKey = null)
        {
            Id = id;
            Key = key;
            Name = name;
            UrlSegment = urlSegment;
            ContentTypeAlias = contentTypeAlias;
            Level = level;
            ParentKey = parentKey;
        }
        public int Id { get; set; }
        public int Level { get; set; }
        public Guid Key { get; set; }
        public Guid? ParentKey { get; set; }
        public string? Name { get; set; }
        public string? UrlSegment { get; set; }
        public string? ContentTypeAlias { get; set; }
    }
}
