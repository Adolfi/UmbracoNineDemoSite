using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemoSite.Core.Features.Shared.Content;
using UmbracoDemoSite.Core.Services;

namespace UmbracoDemoSite.Core.Features.Shared;

public class PageBaseController : RenderController
{
    protected readonly IViewModelService _viewModelService;
    public PageBaseController(
        ILogger<PageBaseController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IViewModelService viewModelService)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _viewModelService = viewModelService;
    }
    protected void MapSitePageBase(SitePageBase pageBase, ISEO currentModel)
    {
        _viewModelService.MapSitePageBase(pageBase, currentModel);
    }
}
