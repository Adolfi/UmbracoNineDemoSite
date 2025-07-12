using Moq;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace UmbracoDemoSite.Tests.Models
{
    public class ContentAndAccessor
    {
        public List<IPublishedContent> Contents { get; set; } = [];
        public List<PublishedContentModel> ContentModels { get; set; } = [];
        public IUmbracoContextAccessor? UmbracoContextAccessor { get; set; }
        public List<ChildrenParentKeyMap> ChildrenParentKeyMaps { get; set; } = [];
    }
}
