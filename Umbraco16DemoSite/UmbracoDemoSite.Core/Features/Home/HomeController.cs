using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoDemoSite.Core.Features.Shared;
using UmbracoDemoSite.Core.Features.Shared.Components.Hero;
using UmbracoDemoSite.Core.Services;
using generatedModels = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Core.Features.Home;

public class HomeController(
    ILogger<HomeController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor,
    IViewModelService viewModelService,
    IPublishedValueFallback publishedValueFallback) : PageBaseController(logger, compositeViewEngine, umbracoContextAccessor, viewModelService)
{
    public IActionResult Home(ContentModel model)
    {
        var mbModel = new generatedModels.Home(model.Content, publishedValueFallback);
        if (mbModel == null)
        { return NotFound(); }

        var viewModel = new HomeViewModel()
        {
            Heading = mbModel.Heading,
            Preamble = mbModel.Preamble,
            BackgroundImage = mbModel.BackgroundImage,
            CallToActionLabel = mbModel.CallToActionLabel,
            CallToActionUrl = mbModel.CallToActionUrl?.Url(),
            Blocks = mbModel.Blocks
        };

        viewModel.Hero = new HeroViewModel()
        {
            CallToActionUrl = viewModel.CallToActionUrl,
            CallToActionLabel = viewModel.CallToActionLabel,
            BackgroundImageUrl = viewModel.BackgroundImage,
            Preamble = viewModel.Preamble,
            Heading = viewModel.Heading
        };

        MapSitePageBase(viewModel, mbModel);

        return View(viewModel);
    }
}