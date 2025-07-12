using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoDemoSite.Core.Services;

namespace UmbracoDemoSite.Core.Features.Shared.Components.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IUmbracoContextAccessor umbracoContextAccessor;
        private readonly IDocumentNavigationQueryService documentNavigationQueryService;
        private readonly IPublishedContentQuery publishedContentQuery;
        private readonly IPublishedContent? root;
        private readonly List<IPublishedContent> topItems = [];
        public NavigationService(
            IUmbracoContextAccessor umbracoContextAccessor, 
            IViewModelService viewModelService,
            IDocumentNavigationQueryService documentNavigationQueryService,
            IPublishedContentQuery publishedContentQuery)
        {
            this.umbracoContextAccessor = umbracoContextAccessor;
            this.documentNavigationQueryService = documentNavigationQueryService;
            this.publishedContentQuery = publishedContentQuery;

            this.umbracoContextAccessor
                .TryGetUmbracoContext(out IUmbracoContext? umbracoContext);

            root = viewModelService.GetRoot();
            if (root != null && root.Id > 0)
            {
                topItems.Add(root);
                var children = viewModelService.GetChildren(root) ?? [];
                topItems.AddRange(children);
            }
        }

        public List<IPublishedContent>? GetSubNavigation(int currentId)
        {
            this.umbracoContextAccessor
                .TryGetUmbracoContext(out IUmbracoContext? umbracoContext);
            var currentPage = umbracoContext?
                .Content
                .GetById(currentId);

            if (currentPage?.Key != null)
            {
                _ = documentNavigationQueryService.TryGetChildrenKeys(currentPage.Key, out var childrenKeys);

                if (childrenKeys != null && childrenKeys.Any())
                {
                    return publishedContentQuery.Content(childrenKeys)?
                        .ToList();
                }
            }

            return null;
        }

        public List<IPublishedContent> GetTopNavigation()
        {
            return topItems;
        }
    }
}
