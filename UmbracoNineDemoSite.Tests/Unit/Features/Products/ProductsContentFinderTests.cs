using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoNineDemoSite.Core;
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

			var umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();

			var container = new Mock<IPublishedContent>();
			container.Setup(s => s.ItemType).Returns(ProductsContainer.ModelItemType);			

			var contentType = new Mock<IPublishedContentType>();
			contentType.Setup(m => m.ItemType).Returns(PublishedItemType.Content);

			var contentCache = new Mock<IPublishedContentCache>();
			contentCache.Setup(request => request.GetByContentType(contentType.Object)).Returns(new List<IPublishedContent>() { container.Object });
			
			//var publishedSnapshot = new Mock<IPublishedSnapshot>();
			//publishedSnapshot.Setup(s => s.Content).Returns(contentCache.Object);

			var publishedSnapshotAccessor = new Mock<IPublishedSnapshotAccessor>();

			var umbracoContextMock = new Mock<IUmbracoContext>();
			umbracoContextMock.Setup(context => context.Content).Returns(contentCache.Object);

			var umbracoContext = umbracoContextMock.Object;
			umbracoContextAccessor.Setup(x => x.TryGetUmbracoContext(out umbracoContext)).Returns(true);

			var productsContentFinder = new ProductsContentFinder(productService.Object, umbracoContextAccessor.Object, publishedSnapshotAccessor.Object);

			var result = productsContentFinder.TryFindContent(request.Object);

			Assert.True(result);
		}
	}
}
