using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation
{
    public interface INavigationService
    {
        List<IPublishedContent> GetTopNavigation();
        List<IPublishedContent> GetSubNavigation(int currentId);
    }
}
