using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core;
using UmbracoNineDemoSite.Core.Features.Products;
using UmbracoNineDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Integrations.Products.Services;
using UmbracoNineDemoSite.Tests.Models;
using UmbracoNineDemoSite.Tests.Unit.Helper;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Products
{
    public class ProductsContentFinderTests
    {
        #region properties
        private delegate void ServiceSetPublishedContent(IPublishedContent content);

        private readonly int _productId = 1001;
        private readonly int _productsContainerId = 1000;
        private readonly Guid _productsContainerKey = Guid.Parse("{d7862f4c-52d6-4811-9a92-19b3f16b94d1}");
        private readonly Guid _rootKey = Guid.Parse("{727f381e-7c10-47a4-bf14-c07a14d83474}");
        #endregion

        #region test cases
        [Test]
        [TestCase("products", 1001, "any")]
        [TestCase("products", 1001, "something")]
        public void Given_RequestContainsExistingProductIdInSecondSegment_When_TryFindContent_Then_ExpectTrue(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(true));
            Assert.That(dummyContent != null);
            Assert.That(dummyContent?.Name, Is.EqualTo(productsContainer?.Object.Name));
            Assert.That(dummyContent?.Id, Is.EqualTo(productsContainer?.Object.Id));
            Assert.That(dummyContent?.Key, Is.EqualTo(productsContainer?.Object.Key));
            Assert.That(dummyContent?.ContentType.Alias, Is.EqualTo(productsContainer?.Object.ContentType.Alias));
            #endregion
        }

        [Test]
        [TestCase("products", 1001, "any", false, true)]
        public void Given_NoProductsContainer_When_TryFindContent_Then_ExpectFalse(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(false));
            Assert.That(dummyContent, Is.EqualTo(null));
            #endregion
        }

        [Test]
        [TestCase("products", 1001, "any", true)]
        public void Given_NoContentAtAll_When_TryFindContent_Then_ExpectFalse(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(false));
            Assert.That(dummyContent, Is.EqualTo(null));
            #endregion
        }

        [Test]
        [TestCase("codegarden/products", 1001, "any")]
        public void Given_RequestContainsExistingProductIdInWrongSegment_When_TryFindContent_Then_ExpectFalse(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(false));
            Assert.That(dummyContent, Is.EqualTo(null));
            #endregion
        }

        [Test]
        [TestCase("products", 1002, "any")]
        public void Given_RequestDoesNotContainExistingProductId_When_TryFindContent_Then_ExpectFalse(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(false));
            Assert.That(dummyContent, Is.EqualTo(null));
            #endregion
        }

        [Test]
        [TestCase("xxx", 1001, "any")]
        public void Given_RequestContainsWrongRootSegment_When_TryFindContent_Then_ExpectFalse(
            string rootPath, int productId, string productName, bool rootKeyNull = false, bool productsContainerKeyNull = false)
        {
            IPublishedContent? dummyContent;
            bool result;
            Mock<ProductsContainer>? productsContainer;
            SetupAndCallTRyFindContent(rootKeyNull, productsContainerKeyNull, rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.That(result, Is.EqualTo(false));
            Assert.That(dummyContent, Is.EqualTo(null));
            #endregion
        }
        #endregion

        private void SetupAndCallTRyFindContent(bool rootKeyNull, bool productsContainerKeyNull, string rootPath, int productId, string productName, out IPublishedContent? foundContent, out bool result, out Mock<ProductsContainer>? productsContainerOut)
        {
            #region setup request Mock
            var absolutePathDecoded = $"/{rootPath}/{productId}/{productName}";
            var request = new Mock<IPublishedRequestBuilder>();
            request.Setup(s => s.AbsolutePathDecoded).Returns(absolutePathDecoded);
            IPublishedContent? callbackContent = null;
            request.Setup(s => s.SetPublishedContent(It.IsAny<IPublishedContent>()))
                .Callback(new ServiceSetPublishedContent((IPublishedContent content) =>
                {
                    callbackContent = content;
                }));
            #endregion

            #region setup IProductService Mock
            var product = new Mock<IProduct>();
            product.Setup(s => s.Id).Returns(_productId);
            product.Setup(s => s.Name).Returns(productName);
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.Get(_productId)).Returns(product.Object);
            var productService = productServiceMock.Object;
            #endregion

            var productContainerDetails = new ContentDetails(_productsContainerId, _productsContainerKey, "Products", "products", ProductsContainer.ModelTypeAlias);
            var helper = new ContentHelper();
            var contentAndAccessor = helper.GetUmbracoContextAccessor<ProductsContainer>([productContainerDetails]);
            if (contentAndAccessor.UmbracoContextAccessor == null)
            {
                throw new InvalidOperationException("UmbracoContextAccessor is not initialized.");
            }

            productsContainerOut = contentAndAccessor.ContentModels.FirstOrDefault() is not ProductsContainer container ? null : Mock.Get(container);

            #region call TryFindContent of ProductsContentFinder
            var documentNavigationQueryService = helper.GetDocumentNavigationServiceMock(rootKeyNull, productsContainerKeyNull, _rootKey, _productsContainerKey);


            var productsContentFinder = new ProductsContentFinder(
                productService,
                contentAndAccessor.UmbracoContextAccessor,
                documentNavigationQueryService);

            result = productsContentFinder.TryFindContent(request.Object).Result;

            foundContent = callbackContent;
            #endregion
        }

    }
}