using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Shared.Extensions;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Page
{
    public class PageController : RenderController
    {
        public PageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) { }

        public IActionResult Page(ContentModel model)
        {
            var mbModel = model.Content as generatedModels.Page ?? new generatedModels.Page(model.Content, null);
            var viewModel = new PageViewModel()
            {
                Heading = mbModel.Heading,
                BodyText = mbModel.BodyText,
                Blocks = mbModel.Blocks
            };
            viewModel.MapSitePageBase(mbModel);

            return View(viewModel);
        }
    }
}