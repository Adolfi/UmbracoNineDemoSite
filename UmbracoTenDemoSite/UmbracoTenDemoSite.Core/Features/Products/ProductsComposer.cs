using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoTenDemoSite.Integrations.Products.Services;

namespace UmbracoTenDemoSite.Core.Features.Products
{
    public class ProductsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.ContentFinders().Append<ProductsContentFinder>();
        }
    }
}
