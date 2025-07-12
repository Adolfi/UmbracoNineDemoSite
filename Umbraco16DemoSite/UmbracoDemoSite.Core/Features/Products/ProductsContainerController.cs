using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoDemoSite.Integrations.Products.Entities;
using UmbracoDemoSite.Integrations.Products.Services;
using UmbracoNineDemoSite.Core.Features.Shared;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using UmbracoNineDemoSite.Core.Models;
using UmbracoNineDemoSite.Core.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductsContainerController(
        ILogger<ProductsContainerController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IProductService productService,
        IViewModelService viewModelService,
        IPublishedValueFallback publishedValueFallback) : PageBaseController(logger, compositeViewEngine, umbracoContextAccessor, viewModelService)
    {
        public IActionResult ProductsContainer(ContentModel model)
        {
            var mbModel = new ProductsContainer(model.Content, publishedValueFallback);
            if (mbModel == null)
            { return NotFound(); }

            var path = UmbracoContext.InPreviewMode ? Request.Path : null;
            var currentProduct = CurrentProduct;

            SitePageBase? viewModel = null;
            var partialViewName = string.Empty;

            if (currentProduct == null)
            {
                partialViewName = "ProductsContainer";
                viewModel = new ProductsContainerViewModel()
                {
                    Products = productService.GetAll()?.Select(product => new ProductPageViewModel(product, path)),
                    Heading = mbModel.Heading
                };
            }
            else
            {
                partialViewName = "ProductPage";
                viewModel = new ProductPageViewModel(currentProduct, path)
                {
                    Heading = mbModel.Heading
                };
            }

            MapSitePageBase(viewModel, mbModel);

            return View(partialViewName, viewModel);
        }

        public IProduct? CurrentProduct
        {
            get
            {
                var segments = Request.Path.Value?.Split("/") ?? [];
                if (segments.Length < 3 || !int.TryParse(segments[2], out int id))
                {
                    return null;
                }

                return productService.Get(id);
            }
        }
    }
}
