using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using UmbracoDemoSite.Core.Features.Shared.Content;
using UmbracoDemoSite.Core.Models;

namespace UmbracoDemoSite.Core.Services;

public class ViewModelService(IDocumentNavigationQueryService documentNavigationQueryService, IPublishedContentQuery publishedContentQuery) : IViewModelService
{
    public void MapSitePageBase(SitePageBase pageBase, ISEO currentModel)
    {
        if(pageBase.Id==0) { pageBase.Id = currentModel.Id; }
        if(pageBase.Key == Guid.Empty) { pageBase.Key = currentModel.Key; }
        pageBase.Name ??= currentModel.Name;
        pageBase.PageTitle ??= currentModel.PageTitle ?? currentModel.Name;
        pageBase.PageDescription ??= currentModel.PageDescription;
        pageBase.SiteName ??= GetRoot(currentModel)?.Name;
    }

    public IPublishedContent? GetRoot(IPublishedContent? content = null)
    {
        IEnumerable<Guid>? ancestorKeys = null;
        if (!documentNavigationQueryService.TryGetRootKeys(out IEnumerable<Guid> rootKeys)) return null;

        if (content != null)
        {
            if (!documentNavigationQueryService.TryGetAncestorsOrSelfKeys(content.Key, out ancestorKeys)) return null;
        }

        var rootKey = rootKeys.FirstOrDefault(x => ancestorKeys == null || ancestorKeys.Contains(x));
        if (rootKey == default) return null;

        return publishedContentQuery.Content(rootKey);
    }

    public IEnumerable<IPublishedContent> GetChildren(IPublishedContent content)
    {
        if (content == null) return [];

        if(documentNavigationQueryService.TryGetChildrenKeys(content.Key, out IEnumerable<Guid> childrenKeys) && childrenKeys.Any())
        {
            return publishedContentQuery.Content(childrenKeys);
        }

        return [];
    }
}
