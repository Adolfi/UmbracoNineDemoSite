using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoNineDemoSite.Integrations.Products.Repositories;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductsComposer : IUserComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();
        }
    }
}