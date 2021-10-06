using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Home;
using UmbracoNineDemoSite.Tests.Extensions;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Home
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            this.controller = new HomeController(Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), Mock.Of<IUmbracoContextAccessor>());
        }
        [Test]
        [TestCase("PageTitle")]
        [TestCase("Other PageTitle")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithPageTitle(string pageTitle)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.PageTitle).ToCamelCase(), pageTitle);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(pageTitle, viewModel.PageTitle);
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithHeading(string heading)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Heading).ToCamelCase(), heading);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(heading, viewModel.Heading);
        }

        [Test]
        [TestCase("Preamble")]
        [TestCase("Other preamble")]
        public void Given_PublishedContentHasPreamble_When_HomeAction_Then_ReturnViewModelWithPreamble(string preamble)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Preamble).ToCamelCase(), preamble);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(preamble, viewModel.Preamble);
        }

        [Test]
        [TestCase("BackgroundImage")]
        [TestCase("Other backgroundImage")]
        public void Given_PublishedContentHasBackgroundImage_When_HomeAction_Then_ReturnViewModelWithBackgroundImage(string backgroundImage)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.BackgroundImage).ToCamelCase(), backgroundImage);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(backgroundImage, viewModel.BackgroundImage);
        }

        [Test]
        [TestCase("CallToActionLabel")]
        [TestCase("Other CallToActionLabel")]
        public void Given_PublishedContentHasCallToActionLabel_When_HomeAction_Then_ReturnViewModelWithCallToActionLabel(string callToActionLabel)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.CallToActionLabel).ToCamelCase(), callToActionLabel);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(callToActionLabel, viewModel.CallToActionLabel);
        }

        [Test]
        public void Given_PublishedContentHasBlocks_When_HomeAction_Then_ReturnViewModelWithBlocks()
        {
            var blockList = new BlockListModel(new List<BlockListItem>());
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Blocks).ToCamelCase(), blockList);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(blockList, viewModel.Blocks);
        }
    }
}
