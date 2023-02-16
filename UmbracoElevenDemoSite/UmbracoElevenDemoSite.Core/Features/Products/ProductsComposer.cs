using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoElevenDemoSite.Integrations.Products.Services;

namespace UmbracoElevenDemoSite.Core.Features.Products
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
