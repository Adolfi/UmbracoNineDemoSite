using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Core.Features.Page;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Tests.Extensions;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Page
{
    [TestFixture]
    public class PageControllerTests
    {
        private PageController controller;
        private Mock<IPublishedContent> publishedContent;

        [SetUp]
        public void SetUp()
        {
            controller = new PageController(Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), Mock.Of<IUmbracoContextAccessor>());
            publishedContent = new Mock<IPublishedContent>();
        }

        [Test]
        [TestCase("Heading")]
        [TestCase("Other heading")]
        public void Given_PublishedContentHasHeading_When_PageAction_Then_ReturnViewModelWithHeading(string heading)
        {            
            publishedContent.SetupPropertyValue(PropertyAlias.Heading, heading);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (PageViewModel)((ViewResult)this.controller.Page(contentModel)).ViewData.Model;

            Assert.AreEqual(heading, viewModel.Heading);
        }

        [Test]
        [TestCase("BodyText")]
        [TestCase("Other BodyText")]
        public void Given_PublishedContentHasBodyText_When_PageAction_Then_ReturnViewModelWithBodyText(string bodyText)
        {
            var bodyTextEncodedHtml = new HtmlEncodedString(bodyText);            
            publishedContent.SetupPropertyValue(PropertyAlias.BodyText, bodyTextEncodedHtml);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (PageViewModel)((ViewResult)this.controller.Page(contentModel)).ViewData.Model;

            Assert.AreEqual(bodyTextEncodedHtml, viewModel.BodyText);
        }

        [Test]
        public void Given_PublishedContentHasBlocks_When_PageAction_Then_ReturnViewModelWithBlocks()
        {
            var blockList = new BlockListModel(new List<BlockListItem>());            
            publishedContent.SetupPropertyValue(PropertyAlias.Blocks, blockList);
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = (PageViewModel)((ViewResult)this.controller.Page(contentModel)).ViewData.Model;

            Assert.AreEqual(blockList, viewModel.Blocks);
        }
    }
}
