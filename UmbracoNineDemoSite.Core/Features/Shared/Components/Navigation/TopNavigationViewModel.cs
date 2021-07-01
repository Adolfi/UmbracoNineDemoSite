using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation
{
    public class TopNavigationViewModel
    {
        public int Selected { get; set; }
        public IEnumerable<IPublishedContent> Items { get; set; }
    }
}
