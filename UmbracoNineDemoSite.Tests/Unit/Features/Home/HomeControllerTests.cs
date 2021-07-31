using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Home;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Tests.Extensions;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Home
{
    [TestFixture]
    public class HomeControllerTests
    {
        private readonly HomeController controller;

        public HomeControllerTests()
        {
            this.controller = new HomeController(Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), Mock.Of<IUmbracoContextAccessor>());
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_IndexAction_Then_ReturnViewModelWithHeading(string heading)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(PropertyAlias.Heading, heading);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(heading, viewModel.Heading);
        }

        [Test]
        [TestCase("Preamble")]
        [TestCase("Other preamble")]
        public void Given_PublishedContentHasPreamble_When_IndexAction_Then_ReturnViewModelWithPreamble(string preamble)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(PropertyAlias.Preamble, preamble);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(preamble, viewModel.Preamble);
        }

        [Test]
        [TestCase("BackgroundImage")]
        [TestCase("Other backgroundImage")]
        public void Given_PublishedContentHasBackgroundImage_When_IndexAction_Then_ReturnViewModelWithBackgroundImage(string backgroundImage)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue(PropertyAlias.BackgroundImage, backgroundImage);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (HomeViewModel)((ViewResult)this.controller.Home(contentModel)).ViewData.Model;

            Assert.AreEqual(backgroundImage, viewModel.BackgroundImage);
        }
    }
}
