using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly UmbracoHelper umbracoHelper;
        private readonly IPublishedContent root;
        private readonly List<IPublishedContent> topItems;
        public NavigationService(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
            root = this.umbracoHelper.ContentAtRoot().FirstOrDefault();
            topItems = new List<IPublishedContent>() { root };
            topItems.AddRange(root.Children);
        }

        public List<IPublishedContent> GetSubNavigation(int currentId)
        {
            var currentPage = this.umbracoHelper.Content(currentId);
            var parentOrSelf = currentPage.AncestorOrSelf(2);
            return parentOrSelf.Children?.ToList();
        }

        public List<IPublishedContent> GetTopNavigation()
        {
            return topItems;
        }
    }
}
