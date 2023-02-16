using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoElevenDemoSite.Core;
using UmbracoElevenDemoSite.Core.Features.Products;
using UmbracoElevenDemoSite.Integrations.Products.Entities;
using UmbracoElevenDemoSite.Integrations.Products.Services;

namespace UmbracoElevenDemoSite.Tests.Unit.Features.Products
{
    public class ProductsContentFinderTests
    {
        #region properties
        private delegate void ServiceTryGetPublishedSnapshot(out IPublishedSnapshot snapshot);
        private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);
        private delegate void ServiceSetPublishedContent(IPublishedContent content);

        private readonly string _productsContainerAlias = "productsContainer";
        private readonly int _productId = 1001;
        #endregion

        [Test]
        [TestCase("products", "1001", "any")]
        [TestCase("products", "1001", "something")]
        public void Given_RequestContainsExistingProductIdInSecondSegment_When_TryFindContent_Then_ExpectTrue(
            string rootPath, string productId, string productName)
        {
            IPublishedContent dummyContent;
            bool result;
            Mock<ProductsContainer> productsContainer;
            SetupAndCallTRyFindContent(rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.True(result);
            Assert.IsNotNull(dummyContent);
            Assert.AreEqual(dummyContent.Name, productsContainer.Object.Name);
            Assert.AreEqual(dummyContent.Id, productsContainer.Object.Id);
            Assert.AreEqual(dummyContent.ContentType.Alias, productsContainer.Object.ContentType.Alias);
            #endregion
        }

        [Test]
        [TestCase("codegarden/products", "1001", "any")]
        public void Given_RequestContainsExistingProductIdInWrongSegment_When_TryFindContent_Then_ExpectFalse(
            string rootPath, string productId, string productName)
        {
            IPublishedContent dummyContent;
            bool result;
            Mock<ProductsContainer> productsContainer;
            SetupAndCallTRyFindContent(rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.False(result);
            Assert.IsNull(dummyContent);
            #endregion
        }

        [Test]
        [TestCase("products", "1002", "any")]
        public void Given_RequestDoesNotContainExistingProductId_When_TryFindContent_Then_ExpectFalse(
            string rootPath, string productId, string productName)
        {
            IPublishedContent dummyContent;
            bool result;
            Mock<ProductsContainer> productsContainer;
            SetupAndCallTRyFindContent(rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.False(result);
            Assert.IsNull(dummyContent);
            #endregion
        }

        [Test]
        [TestCase("xxx", "1001", "any")]
        public void Given_RequestContainsWrongRootSegment_When_TryFindContent_Then_ExpectFalse(
            string rootPath, string productId, string productName)
        {
            IPublishedContent dummyContent;
            bool result;
            Mock<ProductsContainer> productsContainer;
            SetupAndCallTRyFindContent(rootPath, productId, productName, out dummyContent, out result, out productsContainer);

            #region check result of method call
            Assert.False(result);
            Assert.IsNull(dummyContent);
            #endregion
        }

        private void SetupAndCallTRyFindContent(string rootPath, string productId, string productName, out IPublishedContent foundContent, out bool result, out Mock<ProductsContainer> productsContainerOut)
        {
            #region setup request Mock
            var absolutePathDecoded = $"/{rootPath}/{productId}/{productName}";
            var request = new Mock<IPublishedRequestBuilder>();
            request.Setup(s => s.AbsolutePathDecoded).Returns(absolutePathDecoded);
            IPublishedContent callbackContent = null;
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
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Get(_productId)).Returns(product.Object);
            #endregion
            #region setup IUmbracoContextAccessor Mock
            var contentType = Mock.Of<IPublishedContentType>();
            var productsContainerContent = Mock.Of<IPublishedContent>();
            var productsContainerFallback = Mock.Of<IPublishedValueFallback>();
            var productsContainer = new Mock<ProductsContainer>(
                productsContainerContent, productsContainerFallback);
            productsContainer.Setup(s => s.ContentType)
                .Returns(contentType);
            productsContainer.Setup(s => s.Id)
                .Returns(1000);
            productsContainer.Setup(s => s.Name)
                .Returns("Products");
            productsContainer.Setup(s => s.UrlSegment)
                .Returns("products");
            var publishedContentCollection = new List<IPublishedContent>
            {
                productsContainer.Object
            };
            var contentCache = new Mock<IPublishedContentCache>();
            contentCache.Setup(s => s.GetByContentType(contentType))
                .Returns(publishedContentCollection);
            contentCache.Setup(s => s.GetContentType(1))
                .Returns(contentType);
            contentCache.Setup(s => s.GetContentType(_productsContainerAlias))
                .Returns(contentType);
            var umbracoContext = new Mock<IUmbracoContext>();
            umbracoContext.Setup(s => s.Content)
                .Returns(contentCache.Object);

            IUmbracoContext ctx;
            var umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();
            umbracoContextAccessor
                .Setup(x => x.TryGetUmbracoContext(out ctx))
                .Callback(new ServiceTryGetUmbracoContext((out IUmbracoContext uContext) =>
                {
                    uContext = umbracoContext.Object;
                }));
            #endregion
            #region setup IPublishedSnapshotAccessor Mock
            var publishedSnapshot = new Mock<IPublishedSnapshot>();
            publishedSnapshot.Setup(s => s.Content)
                .Returns(contentCache.Object);
            IPublishedSnapshot snapShot;
            var publishedSnapshotAccessor = new Mock<IPublishedSnapshotAccessor>();
            publishedSnapshotAccessor
                .Setup(s => s.TryGetPublishedSnapshot(out snapShot))
                .Callback(new ServiceTryGetPublishedSnapshot((out IPublishedSnapshot snapshot) =>
                {
                    snapshot = publishedSnapshot.Object;
                }))
                .Returns(true);
            #endregion

            #region call TryFindContent of ProductsContentFinder
            var productsContentFinder = new ProductsContentFinder(productService.Object, umbracoContextAccessor.Object, publishedSnapshotAccessor.Object);

            result = productsContentFinder.TryFindContent(request.Object).Result;
            #endregion

            foundContent = callbackContent;
            productsContainerOut = productsContainer;
        }
    }
}



