using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoElevenDemoSite.Core.Features.Shared.Components.Navigation
{
    public class NavigationViewModel
    {
        public int Selected { get; set; }
        public IEnumerable<IPublishedContent> Items { get; set; }
    }
}
