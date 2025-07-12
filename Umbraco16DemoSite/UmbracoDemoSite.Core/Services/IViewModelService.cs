using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using UmbracoNineDemoSite.Core.Models;

namespace UmbracoNineDemoSite.Core.Services
{
    public interface IViewModelService
    {
        void MapSitePageBase(SitePageBase pageBase, ISEO currentModel);
        IPublishedContent? GetRoot(IPublishedContent? content = null);

        IEnumerable<IPublishedContent> GetChildren(IPublishedContent content);
    }
}