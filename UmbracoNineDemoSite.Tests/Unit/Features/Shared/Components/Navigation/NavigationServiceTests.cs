using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Web.Common;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Navigation;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Components.Navigation
{
    public class NavigationServiceTests
    {
        private Mock<ICultureDictionaryFactory> cultureDictionaryFactory;
        private Mock<IUmbracoComponentRenderer> componentRenderer;
        private Mock<IPublishedContentQuery> publishedContentQuery;

        private UmbracoHelper umbracoHelper;
        private NavigationService navigationService;

        [SetUp]
        public void SetUp()
        {
            this.cultureDictionaryFactory = new Mock<ICultureDictionaryFactory>();
            this.componentRenderer = new Mock<IUmbracoComponentRenderer>();
            this.publishedContentQuery = new Mock<IPublishedContentQuery>();

            this.umbracoHelper = new UmbracoHelper(this.cultureDictionaryFactory.Object, this.componentRenderer.Object, this.publishedContentQuery.Object);
            this.navigationService = new NavigationService(this.umbracoHelper);
        }

        [Test]
        [TestCase(123)]
        [TestCase(456)]
        public void Given_CurrentId_When_GetSubNavigation_Then_ReturnSiblingsAsList(int currentId)
        {
            var rootPage = new Mock<IPublishedContent>();
            rootPage.Setup(root => root.Level).Returns(1);

            var parentPage = new Mock<IPublishedContent>();
            parentPage.Setup(parent => parent.Level).Returns(2);
            parentPage.Setup(parent => parent.Parent).Returns(rootPage.Object);

            var currentPage = new Mock<IPublishedContent>();
            currentPage.Setup(current => current.Level).Returns(3);
            currentPage.Setup(current => current.Parent).Returns(parentPage.Object);

            var sibling = Mock.Of<IPublishedContent>();
            var siblings = new List<IPublishedContent>()
            {
                currentPage.Object,
                sibling
            };
            parentPage.Setup(parent => parent.Children).Returns(siblings);
            this.publishedContentQuery.Setup(query => query.Content(currentId)).Returns(currentPage.Object);

            var result = this.navigationService.GetSubNavigation(currentId);

            Assert.AreEqual(siblings, result);
        }

        [Test]
        public void When_GetTopNavigation_Then_ReturnRootChildrenAndSelf()
        {
            var root = new Mock<IPublishedContent>();
            var firstChild = new Mock<IPublishedContent>();
            var secondChild = new Mock<IPublishedContent>();
            var children = new List<IPublishedContent>()
            {
                firstChild.Object,
                secondChild.Object
            };
            root.Setup(x => x.Children).Returns(children);
            this.publishedContentQuery.Setup(query => query.ContentAtXPath($"//{ContentTypeAlias.Home}")).Returns(new List<IPublishedContent>() { root.Object });

            var result = this.navigationService.GetTopNavigation();

            Assert.True(result.Contains(root.Object));
            Assert.True(result.Contains(firstChild.Object));
            Assert.True(result.Contains(secondChild.Object));
        }
    }
}
