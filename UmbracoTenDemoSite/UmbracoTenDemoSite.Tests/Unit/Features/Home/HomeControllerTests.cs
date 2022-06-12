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
        private Mock<IPublishedContent> publishedContent;

        [SetUp]
        public void SetUp()
        {
            controller = new HomeController(Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), Mock.Of<IUmbracoContextAccessor>());
            publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.PageTitle).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.PageDescription).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Preamble).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.BackgroundImage).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Heading).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.CallToActionLabel).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.CallToActionUrl).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.SiteName).ToCamelCase(), string.Empty);
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Blocks).ToCamelCase(), new BlockListModel(new List<BlockListItem>()));
        }

        [Test]
        [TestCase("PageTitle")]
        [TestCase("Other PageTitle")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithPageTitle(string pageTitle)
        {
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.PageTitle).ToCamelCase(), pageTitle);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(pageTitle, viewModel.PageTitle);
        }

        [Test]
        [TestCase("PageDescription")]
        [TestCase("Other PageDescription")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithPageDescrition(string pageDescription)
        {
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.PageDescription).ToCamelCase(), pageDescription);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(pageDescription, viewModel.PageDescription);
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithHeading(string heading)
        {
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
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.CallToActionLabel).ToCamelCase(), callToActionLabel);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(callToActionLabel, viewModel.CallToActionLabel);
        }

        [Test]
        public void Given_PublishedContentHasBlocks_When_HomeAction_Then_ReturnViewModelWithBlocks()
        {
            var blockList = new BlockListModel(new List<BlockListItem>());
            publishedContent.SetupPropertyValue(nameof(HomeViewModel.Blocks).ToCamelCase(), blockList);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(blockList, viewModel.Blocks);
        }
    }
}
