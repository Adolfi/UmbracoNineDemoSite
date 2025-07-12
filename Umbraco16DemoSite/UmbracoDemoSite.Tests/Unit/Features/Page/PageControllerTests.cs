using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using UmbracoDemoSite.Tests.Extensions;
using UmbracoNineDemoSite.Core.Features.Page;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoDemoSite.Tests.Unit.Features.Page
{
    [TestFixture]
    public class PageControllerTests : BaseTestController
    {
        private PageController? controller;

        [SetUp]
        public void SetUp()
        {
            if (umbracoContextAccessor == null)
            {
                throw new InvalidOperationException("UmbracoContextAccessor is not initialized.");
            }
            controller = new PageController(
                Mock.Of<ILogger<PageController>>(),
                compositeViewEngine,
                umbracoContextAccessor,
                viewModelServiceMock.Object,
                fallback
            );

            publishedContentMock.SetupPropertyValue(nameof(PageViewModel.Heading), string.Empty);
            publishedContentMock.SetupPropertyValue(nameof(PageViewModel.BodyText), new HtmlEncodedString("<p></p>"));

            contentModel = new ContentModel(new generatedModels.Page(publishedContentMock.Object, fallback));

            if (contentModel == null)
            { throw new InvalidOperationException("ContentModel is null. Ensure it is initialized before running tests."); }
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_PageAction_Then_ReturnViewModelWithHeading(string heading)
        {
            publishedContentMock.SetupPropertyValue(PropertyAlias.Heading, heading);
            

            IActionResult? actionResult = controller?.Page(contentModel!);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, !Is.Null, "ActionResult should be ViewResult");

            var viewModel = viewResult?.ViewData.Model as PageViewModel;
            Assert.That(viewModel?.Heading, Is.EqualTo(heading));
        }

        [Test]
        [TestCase("BodyText")]
        [TestCase("Other BodyText")]
        public void Given_PublishedContentHasBodyText_When_PageAction_Then_ReturnViewModelWithBodyText(string bodyText)
        {
            var bodyTextEncodedHtml = new HtmlEncodedString(bodyText);
            publishedContentMock.SetupPropertyValue(PropertyAlias.BodyText, bodyTextEncodedHtml);

            IActionResult? actionResult = controller?.Page(contentModel!);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, !Is.Null, "ActionResult should be ViewResult");

            var viewModel = viewResult?.ViewData.Model as PageViewModel;
            Assert.That(viewModel?.BodyText, Is.EqualTo(bodyTextEncodedHtml));
        }

        [Test]
        public void Given_PublishedContentHasBlocks_When_PageAction_Then_ReturnViewModelWithBlocks()
        {
            var blockList = new BlockListModel(new List<BlockListItem>());
            publishedContentMock.SetupPropertyValue(PropertyAlias.Blocks, blockList);

            IActionResult? actionResult = controller?.Page(contentModel!);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, !Is.Null, "ActionResult should be ViewResult");

            var viewModel = viewResult?.ViewData.Model as PageViewModel;
            Assert.That(viewModel?.Blocks, Is.EqualTo(blockList));
        }
    }
}
