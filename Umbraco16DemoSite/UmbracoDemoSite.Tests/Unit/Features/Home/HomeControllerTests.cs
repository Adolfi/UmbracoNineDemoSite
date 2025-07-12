using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using Umbraco.Cms.Core.Models;
using UmbracoDemoSite.Tests.Extensions;
using UmbracoDemoSite.Core.Features.Home;
using UmbracoDemoSite.Core.Features.Shared.Constants;
using generatedModels = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Tests.Unit.Features.Home
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class HomeControllerTests : BaseTestController
    {
        private HomeController? controller;

        [SetUp]
        public void SetUp()
        {
            if (umbracoContextAccessor == null)
            {
                throw new InvalidOperationException("UmbracoContextAccessor is not initialized.");
            }
            controller = new HomeController(
                Mock.Of<ILogger<HomeController>>(), 
                compositeViewEngine, 
                umbracoContextAccessor,
                viewModelService,
                fallback
                ); 

            contentModel = new ContentModel(new generatedModels.Home(publishedContent, fallback));
        }

        [Test]
        [TestCase("PageTitle")]
        [TestCase("Other PageTitle")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithPageTitle(string pageTitle)
        {
            //publishedContent.Setup(p => p.PageTitle).Returns(pageTitle);
            publishedContentMock.SetupPropertyValue(PropertyAlias.PageTitle, pageTitle);

            IActionResult? actionResult = controller?.Home(contentModel!);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, !Is.Null, "ActionResult should be ViewResult");

            var viewModel = viewResult?.ViewData.Model as HomeViewModel;
            Assert.That(viewModel?.PageTitle, Is.EqualTo(pageTitle));
        }
        /*
        [Test]
        [TestCase("PageDescription")]
        [TestCase("Other PageDescription")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithPageDescrition(string pageDescription)
        {
            publishedContent.Setup(p => p.PageDescription).Returns(pageDescription);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.That(Is.Equals(pageDescription, viewModel.PageDescription));
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_HomeAction_Then_ReturnViewModelWithHeading(string heading)
        {
            publishedContent.Setup(p => p.Heading).Returns(heading);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.That(Is.Equals(heading, viewModel.Heading));
        }

        [Test]
        [TestCase("Preamble")]
        [TestCase("Other preamble")]
        public void Given_PublishedContentHasPreamble_When_HomeAction_Then_ReturnViewModelWithPreamble(string preamble)
        {
            publishedContent.Setup(p => p.Preamble).Returns(preamble);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.That(Is.Equals(preamble, viewModel.Preamble));
        }

        [Test]
        [TestCase("BackgroundImage")]
        [TestCase("Other backgroundImage")]
        public void Given_PublishedContentHasBackgroundImage_When_HomeAction_Then_ReturnViewModelWithBackgroundImage(string backgroundImage)
        {
            publishedContent.Setup(p => p.BackgroundImage).Returns(backgroundImage);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.That(Is.Equals(backgroundImage, viewModel.BackgroundImage));
        }

        [Test]
        [TestCase("CallToActionLabel")]
        [TestCase("Other CallToActionLabel")]
        public void Given_PublishedContentHasCallToActionLabel_When_HomeAction_Then_ReturnViewModelWithCallToActionLabel(string callToActionLabel)
        {
            publishedContent.Setup(p => p.CallToActionLabel).Returns(callToActionLabel);
            

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.That(Is.Equals(callToActionLabel, viewModel.CallToActionLabel));
        }

        [Test]
        public void Given_PublishedContentHasBlocks_When_HomeAction_Then_ReturnViewModelWithBlocks()
        {
            var blockList = new BlockListModel(new List<BlockListItem>());
            publishedContent.Setup(p => p.Blocks).Returns(blockList);            

            IActionResult actionResult = this.controller.Home(contentModel);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, !Is.Null, "ActionResult should be ViewResult");

            var viewModel = viewResult?.ViewData.Model as HomeViewModel;
            Assert.That(viewModel?.Blocks, Is.EqualTo(blockList), "viewModel.Blocks are not valid");
        }
        */
    }
}
