using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Linq;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductsContainerController : RenderController
    {
        private readonly IProductService productService;

        public ProductsContainerController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IProductService productService) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            this.productService = productService;
        }

        public override IActionResult Index()
        {
            var viewModel = new ProductsContainerViewModel(CurrentPage)
            {
                Products = this.productService.GetAll().Select(product => new ProductPageViewModel(product))
            };
            return CurrentTemplate(viewModel);
        }
    }
}
