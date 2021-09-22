using System.Linq;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    /// <summary>
    /// Docs: https://our.umbraco.com/Documentation/Reference/Routing/Request-Pipeline/IContentFinder
    /// </summary>
    public class ProductsContentFinder : IContentFinder
    {
        private readonly IProductService productService;
        private readonly IUmbracoContextAccessor umbracoContextAccessor;

        public ProductsContentFinder(IProductService productService, IUmbracoContextAccessor umbracoContextAccessor)
        {
            this.productService = productService;
            this.umbracoContextAccessor = umbracoContextAccessor;
        }

        public bool TryFindContent(IPublishedRequestBuilder request)
        {
            var segments = request.AbsolutePathDecoded.Split("/");
            if (!int.TryParse(segments[2], out var id))
            {
                return false;
            }

            var product = this.productService.Get(id);
            if (product == null)
            {
                return false;
            }

            umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);
            var container = umbracoContext?.Content.GetByXPath($"//{ContentTypeAlias.ProductsContainer}")?.FirstOrDefault();
            if (container == null)
            {
                return false;
            }

            request.SetPublishedContent(container);
            return true;
        }
    }
}
