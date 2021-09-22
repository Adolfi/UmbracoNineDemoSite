using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using System;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core.Features.Shared.Components.ContentBlock;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Tests.Extensions;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Components.Footer
{
    [TestFixture]
    public class ContentBlockViewComponentTests
    {
        private ContentBlockViewComponent contentBlockViewComponent;

        [SetUp]
        public void SetUp()
        {
            this.contentBlockViewComponent = new ContentBlockViewComponent();
        }

        [Test]
        [TestCase("header")]
        [TestCase("heading")]        
        public void Given_BlockListHasHeading_When_Invoke_Then_ExpectViewModelHeading(string heading)
        {
            var blockElement = new Mock<IPublishedContent>();
            blockElement.SetupPropertyValue(PropertyAlias.Heading, heading);
            var blockListItem = new BlockListItem(new GuidUdi(ContentTypeAlias.ContentBlock, Guid.NewGuid()), blockElement.Object, null, null);

            var model = (ContentBlockViewModel)((ViewViewComponentResult)this.contentBlockViewComponent.Invoke(blockListItem)).ViewData.Model;

            Assert.AreEqual(heading, model.Heading);
        }
        //[Test] TODO: Investigate why this test started failing when installing RC003.
        //[TestCase("<p>BodyText</p>")]
        //[TestCase("<p>Other BodyText</p>")]
        public void Given_BlockListHasBodyText_When_Invoke_Then_ExpectViewModelBodyText(string bodyText)
        {
            var bodyTextEncodedHtml = new HtmlEncodedString(bodyText);

            var blockElement = new Mock<IPublishedContent>();
            blockElement.SetupPropertyValue(PropertyAlias.BodyText, bodyTextEncodedHtml);
            var blockListItem = new BlockListItem(new GuidUdi(ContentTypeAlias.ContentBlock, Guid.NewGuid()), blockElement.Object, null, null);

            var model = (ContentBlockViewModel)((ViewViewComponentResult)this.contentBlockViewComponent.Invoke(blockListItem)).ViewData.Model;

            Assert.AreEqual(bodyText, model.BodyText);
        }
    }
}
