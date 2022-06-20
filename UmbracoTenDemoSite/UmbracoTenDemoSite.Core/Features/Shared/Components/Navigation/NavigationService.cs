using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace UmbracoTenDemoSite.Core.Features.Shared.Components.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IUmbracoContextAccessor umbracoContextAccessor;
        private readonly IPublishedContent root;
        private readonly List<IPublishedContent> topItems;
        public NavigationService(IUmbracoContextAccessor umbracoContextAccessor)
        {
            this.umbracoContextAccessor = umbracoContextAccessor;

            this.umbracoContextAccessor
                .TryGetUmbracoContext(out IUmbracoContext umbracoContext);

            root = umbracoContext?
                .Content
                .GetAtRoot()
                .FirstOrDefault();

            topItems = new List<IPublishedContent>() { root };
            topItems.AddRange(root.Children);
        }

        public List<IPublishedContent> GetSubNavigation(int currentId)
        {
            this.umbracoContextAccessor
                .TryGetUmbracoContext(out IUmbracoContext umbracoContext);
            var currentPage = umbracoContext
                .Content
                .GetById(currentId);
            var parentOrSelf = currentPage.AncestorOrSelf(2);
            return parentOrSelf.Children?.ToList();
        }

        public List<IPublishedContent> GetTopNavigation()
        {
            return topItems;
        }
    }
}
