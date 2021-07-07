using System;
using Umbraco.Cms.Core.Routing;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductPageContentFinder : IContentFinder
    {
        private readonly IProductService productService;

        public ProductPageContentFinder(IProductService productService)
        {
            this.productService = productService;
        }

        public bool TryFindContent(IPublishedRequestBuilder request)
        {
            var path = request.Uri.GetAbsolutePathDecoded();
            if (path.Contains("products"))
            {
                var segments = request.Uri.Segments;
                if (int.TryParse(segments[2].Trim("/"), out var productId))
                {
                    var product = this.productService.Get(productId);
                    throw new Exception("TODO");
                }
            }

            return true;
        }
    }
}
