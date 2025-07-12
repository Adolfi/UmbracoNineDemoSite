using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoDemoSite.Core.Models
{
    public interface IPublishedContentExtended
    {
        string? PageDescription { get; set; }
        string? PageTitle { get; set; }

        #region partial IPublishedContent nullable properties
        int Id { get; set; }

        string Name { get; set; }

        string? UrlSegment { get; set; }

        int SortOrder { get; set; }

        int Level { get; set; }

        string Path { get; set; }

        int? TemplateId { get; set; }

        int CreatorId { get; set; }

        DateTime? CreateDate { get; set; }

        int WriterId { get; set; }

        DateTime? UpdateDate { get; set; }

        IReadOnlyDictionary<string, PublishedCultureInfo>? Cultures { get; set; }

        PublishedItemType? ItemType { get; set; }

        IPublishedContent? Parent { get; set; }

        IEnumerable<IPublishedContent>? Children { get; set; }

        IPublishedContentType? ContentType { get; set; }

        Guid? Key { get; set; }

        IEnumerable<IPublishedProperty>? Properties { get; set; }
        #endregion
    }
    public class PublishedContentExtended : IPublishedContentExtended
    {
        public string? PageDescription { get; set; }
        public string? PageTitle { get; set; }

        #region partial IPublishedContent nullable properties
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? UrlSegment { get; set; }

        public int SortOrder { get; set; }

        public int Level { get; set; }

        public string Path { get; set; } = string.Empty;

        public int? TemplateId { get; set; }

        public int CreatorId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int WriterId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public IReadOnlyDictionary<string, PublishedCultureInfo>? Cultures { get; set; }

        public PublishedItemType? ItemType { get; set; }

        public IPublishedContent? Parent { get; set; }

        public IEnumerable<IPublishedContent>? Children { get; set; }

        public IPublishedContentType? ContentType { get; set; }

        public Guid? Key { get; set; }

        public IEnumerable<IPublishedProperty>? Properties { get; set; }
        #endregion
    }
}
