using System.Collections.Generic;
using System.Linq;
using Examine;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Core.Features.Search.Examine.Index.ContentValueSetBuilder
{
    public class ProductValueSetBuilder : IValueSetBuilder<IProduct>
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public ProductValueSetBuilder(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }
        public IEnumerable<ValueSet> GetValueSets(params IProduct[] content)
        {
            if (!content.Any()) return null;

            var valueSet = BuildValueSet(content);

            return valueSet;
        }

        private IEnumerable<ValueSet> BuildValueSet(IProduct[] products)
        {
            using (var umbCxt = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var containerPage = umbCxt.UmbracoContext.Content.GetSingleByXPath($"//{PropertyAlias.ProductsContainer}");
                foreach (var product in products)
                {
                    var relativePath = $"/{containerPage.UrlSegment()}/{product.Id}/{product.Name}";

                    var values = new Dictionary<string, IEnumerable<object>>()
                    {
                        {nameof(Product.Id).ToFirstLower(), new object[] { product.Id }},
                        {"type", new object[] { ContentTypeAlias.ExternalProduct }},
                        {"url", new [] { relativePath }},
                        {nameof(Product.Name).ToFirstLower(), new object[] { product.Name }},
                        {nameof(Product.Description).ToFirstLower(), new object[] { product.Description }},
                        {nameof(Product.ImageUrl).ToFirstLower(), new object[] { product.ImageUrl }},
                        {nameof(Product.Price).ToFirstLower(), new object[] { product.Price }}
                    };

                    var valueSet = new ValueSet(product.Id.ToString(), IndexTypes.Content, ContentTypeAlias.ExternalProduct, values);

                    yield return valueSet;
                }
            }
        }
    }
}
