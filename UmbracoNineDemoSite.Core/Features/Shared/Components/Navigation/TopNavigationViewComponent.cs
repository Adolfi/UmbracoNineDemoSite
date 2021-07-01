using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation
{
    [ViewComponent(Name = "TopNavigation")]
    public class TopNavigationViewComponent : ViewComponent
    {
        private readonly UmbracoHelper umbracoHelper;

        public TopNavigationViewComponent(UmbracoHelper umbracoHelper)
        {
            this.umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke(int selected)
        {
            var root = this.umbracoHelper.ContentAtXPath($"//{ContentTypeAlias.Home}")?.FirstOrDefault();
            var items = new List<IPublishedContent>(){ root };
            items.AddRange(root.Children);

            return View(new TopNavigationViewModel()
            {
                Selected = selected,
                Items = items
        });
        }
    }
}
