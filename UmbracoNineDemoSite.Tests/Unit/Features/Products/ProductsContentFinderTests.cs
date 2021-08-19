using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using UmbracoNineDemoSite.Core.Features.Products;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Products
{
    public class ProductsContentFinderTests
    {
        [Test]        
        [TestCase(123, "any")]
        [TestCase(456, "product")]
        [TestCase(789, "name")]
        public void Given_RequestContainsExistingProductId_When_TryFindContent_Then_ExpectTrue(int productId, string productName)
        {
            var absolutePathDecoded = $"/products/{productId}/{productName}";
            var request = new Mock<IPublishedRequestBuilder>();
            request.Setup(x => x.AbsolutePathDecoded).Returns(absolutePathDecoded);

            var product = Mock.Of<IProduct>();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Get(productId)).Returns(product);

            var container = Mock.Of<IPublishedContent>();
            var contentQuery = new Mock<IPublishedContentQuery>();
            contentQuery.Setup(x => x.ContentAtXPath($"//{ContentTypeAlias.ProductsContainer}")).Returns(new List<IPublishedContent>() { container });

            // WIP!

            //var productsContentFinder = new ProductsContentFinder(productService.Object);

            //var result = productsContentFinder.TryFindContent(request.Object);

            //Assert.True(result);


        }
    }
}
