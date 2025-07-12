using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;
using UmbracoDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Core.Features.Search.Examine.Index.ContentValueSetBuilder
{
    public class ProductValueSetBuilder : IValueSetBuilder<IProduct>
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IDocumentNavigationQueryService _documentNavigationQueryService;

        public ProductValueSetBuilder(IUmbracoContextFactory umbracoContextFactory, IUmbracoContextAccessor umbracoContextAccessor, IDocumentNavigationQueryService documentNavigationQueryService)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _documentNavigationQueryService = documentNavigationQueryService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }
        public IEnumerable<ValueSet> GetValueSets(params IProduct[] content)
        {
            if (!content.Any()) return [];

            var valueSet = BuildValueSet(content);

            return valueSet;
        }

        private IEnumerable<ValueSet> BuildValueSet(IProduct[] products)
        {
            using (var umbCxt = _umbracoContextFactory.EnsureUmbracoContext())
            {
                IPublishedContent? containerPage = null;
                if (_documentNavigationQueryService.TryGetRootKeys(out IEnumerable<Guid> rootKeys))
                {
                    foreach (var rootKey in rootKeys)
                    {

                        if (_documentNavigationQueryService
                            .TryGetDescendantsKeysOfType(rootKey, ProductsContainer.ModelTypeAlias, out IEnumerable<Guid> containerKeys))
                        {
                            _umbracoContextAccessor
                                .TryGetUmbracoContext(out IUmbracoContext? umbracoContext);
                            containerPage = umbracoContext?
                                .Content
                                .GetById(containerKeys.FirstOrDefault());
                            break;
                        }
                    }
                }

                if (containerPage != null && products != null && products.Any())
                {
                    var externalProductType = nameof(Product).ToFirstLower();
                    foreach (var product in products)
                    {
                        var relativePath = $"/{containerPage.UrlSegment()}/{product.Id}/{product.Name}";

                        var values = new Dictionary<string, IEnumerable<object>>()
                    {
                        {nameof(Product.Id).ToFirstLower(), new object[] { product.Id }},
                        {"type", new object[] { externalProductType } },
                        {"url", new [] { relativePath }},
                        {nameof(Product.Name).ToFirstLower(), new object[] { product.Name ?? string.Empty }},
                        {nameof(Product.Description).ToFirstLower(), new object[] { product.Description ?? string.Empty }},
                        {nameof(Product.ImageUrl).ToFirstLower(), new object[] { product.ImageUrl ?? string.Empty }},
                        {nameof(Product.Price).ToFirstLower(), new object[] { product.Price }}
                    };

                        var valueSet = new ValueSet(product.Id.ToString(), IndexTypes.Content, externalProductType, values);

                        yield return valueSet;
                    }
                }
            }
        }
    }
}
