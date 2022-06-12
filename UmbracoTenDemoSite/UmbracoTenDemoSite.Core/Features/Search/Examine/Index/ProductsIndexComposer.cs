using Examine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoTenDemoSite.Core.Features.Search.Examine.Index.ContentValueSetBuilder;
using UmbracoTenDemoSite.Core.Features.Search.Examine.Index.IndexPopulators;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using UmbracoTenDemoSite.Integrations.Products.Entities;
using UmbracoTenDemoSite.Integrations.Products.Services;

namespace UmbracoTenDemoSite.Core.Features.Search.Examine.Index
{
    public class ProductsIndexComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddExamineLuceneIndex<ProductsUmbracoIndex, ConfigurationEnabledDirectoryFactory>(IndexNames.ProductsIndex);

            builder.Services.AddSingleton<IValueSetBuilder<IProduct>>(factory =>
                new ProductValueSetBuilder(factory.GetRequiredService<IUmbracoContextFactory>()));

            builder.Services.AddSingleton<IIndexPopulator>(factory => new ProductsIndexPopulator(
                factory.GetRequiredService<IProductService>(),
                factory.GetRequiredService<IValueSetBuilder<IProduct>>()));

        }
    }
}
