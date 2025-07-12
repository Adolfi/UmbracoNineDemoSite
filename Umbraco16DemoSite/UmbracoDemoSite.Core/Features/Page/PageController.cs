using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core.Features.Shared;
using UmbracoNineDemoSite.Core.Models;
using UmbracoNineDemoSite.Core.Services;
using GM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Page;

public class PageController(
    ILogger<PageController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor,
    IViewModelService viewModelService,
    IPublishedValueFallback publishedValueFallback) : PageBaseController(logger, compositeViewEngine, umbracoContextAccessor, viewModelService)
{
    public IActionResult Page(ContentModel model)
    {
        var mbModel = new GM.Page(model.Content, publishedValueFallback);
        if (mbModel == null)
        { return NotFound(); }

        var viewModel = new PageViewModel()
        {
            Heading = mbModel.Heading,
            BodyText = mbModel.BodyText,
            Blocks = mbModel.Blocks
        };

        MapSitePageBase(viewModel, mbModel);

        return View(viewModel);
    }
}