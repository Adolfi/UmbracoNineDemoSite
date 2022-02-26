using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core;
using UmbracoNineDemoSite.Core.Features.Products;
using UmbracoNineDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Integrations.Products.Services;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Products
{
    public class ProductsContentFinderTests
    {
        #region properties
        private delegate void ServiceTryGetPublishedSnapshot(out IPublishedSnapshot snapshot);
        private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);
        private delegate void ServiceSetPublishedContent(IPublishedContent content);

        private readonly string productsContainerAlias = "productsContainer";
        #endregion

        [Test]
        [TestCase("products", 123, "any")]
        [TestCase("products", 456, "product")]
        [TestCase("products", 789, "name")]
        public void Given_RequestContainsExistingProductId_When_TryFindContent_Then_ExpectTrue(
            string rootPath, int productId, string productName)
        {
            #region setup request Mock
            var absolutePathDecoded = $"/{rootPath}/{productId}/{productName}";
            var request = new Mock<IPublishedRequestBuilder>();
            request.Setup(s => s.AbsolutePathDecoded).Returns(absolutePathDecoded);
            IPublishedContent dummyContent = null;
            request.Setup(s => s.SetPublishedContent(It.IsAny<IPublishedContent>()))
                .Callback(new ServiceSetPublishedContent((IPublishedContent content) =>
                {
                    dummyContent = content;
                }));
            #endregion
            #region setup IProductService Mock
            var product = new Mock<IProduct>();
            product.Setup(s => s.Id).Returns(productId);
            product.Setup(s => s.Name).Returns(productName);
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.Get(productId)).Returns(product.Object);
            #endregion
            #region setup IUmbracoContextAccessor Mock
            var contentType = new Mock<IPublishedContentType>();
            contentType.Setup(s => s.Alias)
                .Returns(productsContainerAlias);
            contentType.Setup(s => s.Id)
                .Returns(1);
            contentType.Setup(m => m.ItemType)
                .Returns(PublishedItemType.Content);
            var productsContainerContent = Mock.Of<IPublishedContent>();
            var productsContainerFallback = Mock.Of<IPublishedValueFallback>();
            var productsContainer = new Mock<ProductsContainer>(
                productsContainerContent, productsContainerFallback);
            productsContainer.Setup(s => s.ContentType)
                .Returns(contentType.Object);
            productsContainer.Setup(s => s.Id)
                .Returns(1000);
            productsContainer.Setup(s => s.Name)
                .Returns("Products");
            var publishedContentCollection = new List<IPublishedContent>
            {
                productsContainer.Object
            };
            var contentCache = new Mock<IPublishedContentCache>();
            contentCache.Setup(s => s.GetByContentType(contentType.Object))
                .Returns(publishedContentCollection);
            contentCache.Setup(s => s.GetContentType(1))
                .Returns(contentType.Object);
            contentCache.Setup(s => s.GetContentType(productsContainerAlias))
                .Returns(contentType.Object);
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

            var result = productsContentFinder.TryFindContent(request.Object);
            #endregion

            #region check result of method call
            Assert.True(result);
            Assert.IsNotNull(dummyContent);
            Assert.AreEqual(dummyContent.Name, productsContainer.Object.Name);
            Assert.AreEqual(dummyContent.Id, productsContainer.Object.Id);
            Assert.AreEqual(dummyContent.ContentType.Alias, productsContainer.Object.ContentType.Alias);
            #endregion
        }
    }
}



