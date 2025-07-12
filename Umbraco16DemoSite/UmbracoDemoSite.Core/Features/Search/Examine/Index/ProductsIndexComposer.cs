using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoDemoSite.Integrations.Products.Entities;
using UmbracoDemoSite.Integrations.Products.Services;
using UmbracoDemoSite.Core.Features.Search.Examine.Index.ContentValueSetBuilder;
using UmbracoDemoSite.Core.Features.Search.Examine.Index.IndexPopulators;
using UmbracoDemoSite.Core.Features.Shared.Constants;

namespace UmbracoDemoSite.Core.Features.Search.Examine.Index
{
    public class ProductsIndexComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddExamineLuceneIndex<ProductsUmbracoIndex, ConfigurationEnabledDirectoryFactory>(IndexNames.ProductsIndex);

            builder.Services.AddSingleton<IValueSetBuilder<IProduct>>(factory =>
                new ProductValueSetBuilder(
                    factory.GetRequiredService<IUmbracoContextFactory>(),
                    factory.GetRequiredService<IUmbracoContextAccessor>(),
                    factory.GetRequiredService<IDocumentNavigationQueryService>()
                    ));

            builder.Services.AddSingleton<IIndexPopulator>(factory => new ProductsIndexPopulator(
                factory.GetRequiredService<IProductService>(),
                factory.GetRequiredService<IValueSetBuilder<IProduct>>()));

        }
    }
}
