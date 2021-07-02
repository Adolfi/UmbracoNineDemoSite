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

        public NavigationService(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
        }

        public List<IPublishedContent> GetSubNavigation(int currentId)
        {
            var currentPage = this.umbracoHelper.Content(currentId);
            var parentOrSelf = currentPage.AncestorOrSelf(2);
            return parentOrSelf.Children?.ToList();
        }

        public List<IPublishedContent> GetTopNavigation()
        {
            var root = this.umbracoHelper.ContentAtXPath($"//{ContentTypeAlias.Home}")?.FirstOrDefault();
            var items = new List<IPublishedContent>() { root };
            items.AddRange(root.Children);
            return items;
        }
    }
}
