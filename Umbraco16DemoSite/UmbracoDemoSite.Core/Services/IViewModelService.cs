using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoDemoSite.Core.Features.Shared.Content;
using UmbracoDemoSite.Core.Models;

namespace UmbracoDemoSite.Core.Services
{
    public interface IViewModelService
    {
        void MapSitePageBase(SitePageBase pageBase, ISEO currentModel);
        IPublishedContent? GetRoot(IPublishedContent? content = null);

        IEnumerable<IPublishedContent> GetChildren(IPublishedContent content);
    }
}