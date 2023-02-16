using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using UmbracoElevenDemoSite.Core.Features.Shared.Components.Navigation;
using UmbracoElevenDemoSite.Core.Features.Shared.Constants;

namespace UmbracoElevenDemoSite.Tests.Unit.Features.Shared.Components.Navigation
{
    public class NavigationServiceTests
    {
        private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);
        private delegate void ServiceSetPublishedContentById(int id);

        private Mock<IPublishedContentCache> contentCache;
        private NavigationService navigationService;

        private IEnumerable<IPublishedContent> topItems = new List<IPublishedContent>();
        private List<IPublishedContent> allPages = new List<IPublishedContent>();

        [SetUp]
        public void SetUp()
        {
            var firstSubPage = new Mock<IPublishedContent>();
            firstSubPage.Setup(s => s.Id).Returns(1011);
            firstSubPage.Setup(s => s.Level).Returns(3);
            firstSubPage.Setup(s => s.Name).Returns("First Subpage");

            var secondSubPage = new Mock<IPublishedContent>();
            secondSubPage.Setup(s => s.Id).Returns(1012);
            secondSubPage.Setup(s => s.Level).Returns(3);
            secondSubPage.Setup(s => s.Name).Returns("Second Subpage");

            var aboutChildren = new List<IPublishedContent>()
            {
                firstSubPage.Object,
                secondSubPage.Object,
            };
            var aboutPage = new Mock<IPublishedContent>();
            aboutPage.Setup(s => s.Id).Returns(1001);
            aboutPage.Setup(s => s.Level).Returns(2);
            aboutPage.Setup(s => s.Name).Returns("About us");
            aboutPage.Setup(s => s.Children).Returns(aboutChildren);

            firstSubPage.Setup(s => s.Parent).Returns(aboutPage.Object);
            secondSubPage.Setup(s => s.Parent).Returns(aboutPage.Object);

            var productsPage = new Mock<IPublishedContent>();
            productsPage.Setup(s => s.Id).Returns(1002);
            productsPage.Setup(s => s.Level).Returns(2);
            productsPage.Setup(s => s.Name).Returns("Products");

            var roorChildren = new List<IPublishedContent>()
            {
                aboutPage.Object,
                productsPage.Object,
            };
            var root = new Mock<IPublishedContent>();
            root.Setup(s => s.Id).Returns(1000);
            root.Setup(s => s.Level).Returns(1);
            root.Setup(s => s.Name).Returns("Home");
            root.Setup(s => s.Children).Returns(roorChildren);
            var pages = new List<IPublishedContent>()
            {
                root.Object,
                aboutPage.Object,
                productsPage.Object,
            };
            topItems = pages;
            allPages.AddRange(pages);
            allPages.AddRange(aboutChildren);

            var roots = new List<IPublishedContent>();
            roots.Add(root.Object);

            var contentType = Mock.Of<IPublishedContentType>();

            contentCache = new Mock<IPublishedContentCache>();
            contentCache.Setup(s => s.GetAtRoot(null))
                .Returns(pages);
            
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
            navigationService = new NavigationService(umbracoContextAccessor.Object);
        }

        [Test]
        [TestCase(1011)]
        [TestCase(1012)]
        public void Given_CurrentId_When_GetSubNavigation_Then_ReturnSiblingsAsList(int currentId)
        {
            contentCache.Setup(s => s.GetById(currentId))
              .Returns(allPages.FirstOrDefault(c => c.Id == currentId));

            var result = navigationService.GetSubNavigation(currentId);

            var currentContent = allPages.FirstOrDefault(c => c.Id == currentId);
            var parent = currentContent.Parent;
            var siblings = parent.Children;
            Assert.AreEqual(siblings, result);
        }

        [Test]
        public void When_GetTopNavigation_Then_ReturnRootChildrenAndSelf()
        {
            var result = navigationService.GetTopNavigation();

            var root = topItems.FirstOrDefault(c => c.Level == 1);
            var firstChild = root.Children.ElementAt(0);
            var secondChild = root.Children.ElementAt(1);

            Assert.True(result.Contains(root));
            Assert.True(result.Contains(firstChild));
            Assert.True(result.Contains(secondChild));
        }
    }
}
