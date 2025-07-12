using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoDemoSite.Integrations.Products.Services;

namespace UmbracoDemoSite.Core.Features.Products
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
