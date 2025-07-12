using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;
using UmbracoDemoSite.Core.Features.Shared.Components.Navigation;
using UmbracoDemoSite.Tests.Models;
using UmbracoDemoSite.Tests.Unit.Helper;
using GM = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Tests.Unit.Features.Shared.Components.Navigation
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class NavigationServiceTests : BaseViewModelController
    {
        private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);
        private delegate void ServiceSetPublishedContentById(int id);

        private delegate IEnumerable<IPublishedContent> ServiceGetPublishedContents(IPublishedContent content);

        private Mock<IPublishedContentCache> contentCache = new();
        private NavigationService? navigationService;

        protected ContentModel? contentModel;

        [SetUp]
        public void SetUp()
        {
            if (umbracoContextAccessor == null)
            {
                throw new InvalidOperationException("UmbracoContextAccessor is not initialized.");
            }
            navigationService = new NavigationService(
                umbracoContextAccessor,
                viewModelService,
                documentNavigationQueryService,
                publishedContentQuery);
        }

        [Test]
        [TestCase(1000)]
        [TestCase(1001)]
        public void Given_CurrentId_When_GetSubNavigation_Then_ReturnSiblingsAsList(int currentId)
        {
            contentCache.Setup(s => s.GetById(currentId))
              .Returns(allPages.FirstOrDefault(c => c.Id == currentId));

            var currentContent = allPages.FirstOrDefault(c => c.Id == currentId);
            var currentContentDetails = contentDetails
                .Where(c => c.Id == currentId)
                .FirstOrDefault();
            var siblingsDetails = contentDetails
                .Where(c => c.ParentKey == currentContentDetails?.Key)
                .Select(c => c.Key)
                .ToList();
            var siblingKeys = allPages
                .Where(c => siblingsDetails.Contains(c.Key))
                .Select(c => c.Key)
                .ToList();

            var result = navigationService?.GetSubNavigation(currentId);
            var resultKeys = result?
                .Select(c => c.Key)
                .ToList();

            Assert.That(siblingKeys, Is.EqualTo(resultKeys));
        }

        [Test]
        public void When_GetTopNavigation_Then_ReturnRootChildrenAndSelf()
        {
            var result = navigationService?.GetTopNavigation();

            var root = topItems.FirstOrDefault(c => c.Level == 1);
            var childrenDetails = contentDetails
                .Where(c => c.Level == 2 && c.ParentKey != null && c.ParentKey == root?.Key)
                .Select(c => c.Key)
                .ToArray();

            var firstChild = childrenDetails.Length < 1
                ? null
                : allPages.FirstOrDefault(c => childrenDetails[0].Equals(c.Key));

            var secondChild = childrenDetails.Length < 2
                ? null
                : allPages.FirstOrDefault(c => childrenDetails[1].Equals(c.Key));

            var containsRoot = root != null && (result?.Contains(root) ?? false);
            var containsFirstChild = firstChild != null && (result?.Contains(firstChild) ?? false);
            var containsSecondChild = secondChild != null && (result?.Contains(secondChild) ?? false);

            Assert.That(Equals(containsRoot, true));
            Assert.That(Equals(containsFirstChild, true));
            Assert.That(Equals(containsSecondChild, true));
        }
    }
}
