using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoNineDemoSite.Core.Features.Home
{
    public class HomeController : RenderController
    {
        public HomeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) { }

        public override IActionResult Index()
        {
            return CurrentTemplate(new HomeViewModel(CurrentPage));
        }
    }
}