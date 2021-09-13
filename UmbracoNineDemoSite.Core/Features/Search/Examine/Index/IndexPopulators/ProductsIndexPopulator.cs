using System.Collections.Generic;
using System.Linq;
using Examine;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoNineDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Core.Features.Search.Examine.Index.IndexPopulators
{
    public class ProductsIndexPopulator : IndexPopulator<ProductsUmbracoIndex>
    {
        private readonly IProductService _productsService;
        private readonly IValueSetBuilder<IProduct> _productValueSetBuilder;
        public ProductsIndexPopulator(IProductService productsService, IValueSetBuilder<IProduct> productValueSetBuilder)
        {
            _productsService = productsService;
            _productValueSetBuilder = productValueSetBuilder;
        }
        protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
        {
            IndexProducts(indexes);
        }

        private void IndexProducts(IReadOnlyList<IIndex> indexes)
        {
            var products = _productsService.GetAll();
            var valueSet = _productValueSetBuilder.GetValueSets(products.ToArray());

            foreach (var index in indexes)
            {
                index.IndexItems(valueSet);
            }
        }
    }
}
