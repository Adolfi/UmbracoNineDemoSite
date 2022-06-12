using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using System;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core;
using UmbracoNineDemoSite.Core.Features.Shared.Components.ContentBlock;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Tests.Extensions;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Components.Footer
{
	[TestFixture]
	public class ContentBlockViewComponentTests
	{
		private ContentBlockViewComponent contentBlockViewComponent;
		private Mock<IPublishedContent> element;

		[SetUp]
		public void SetUp()
		{
			this.contentBlockViewComponent = new ContentBlockViewComponent();
			element = new Mock<IPublishedContent>();
			element.SetupPropertyValue(nameof(generatedModels.Page.Heading).ToCamelCase(), string.Empty);
			element.SetupPropertyValue(nameof(generatedModels.Page.BodyText).ToCamelCase(), string.Empty);
		}

		[Test]
		[TestCase("header")]
		[TestCase("heading")]
		public void Given_BlockListHasHeading_When_Invoke_Then_ExpectViewModelHeading(string heading)
		{
			element.SetupPropertyValue(nameof(generatedModels.Page.Heading).ToCamelCase(), heading);
			var blockElement = new generatedModels.ContentBlock(element.Object as IPublishedElement, null);
			var blockListItem = new BlockListItem(new GuidUdi(ContentBlock.ModelTypeAlias, Guid.NewGuid()), blockElement, null, null);

			var model = (ContentBlockViewModel)((ViewViewComponentResult)this.contentBlockViewComponent.Invoke(blockListItem)).ViewData.Model;

			Assert.AreEqual(heading, model.Heading);
		}

		[Test]
		[TestCase("<p>BodyText</p>")]
		[TestCase("<p>Other BodyText</p>")]
		public void Given_BlockListHasBodyText_When_Invoke_Then_ExpectViewModelBodyText(string bodyText)
		{
			var bodyTextEncodedHtml = new HtmlEncodedString(bodyText);

			element.SetupPropertyValue(nameof(generatedModels.Page.BodyText).ToCamelCase(), bodyTextEncodedHtml);
			var blockElement = new generatedModels.ContentBlock(element.Object as IPublishedElement, null);
			var blockListItem = new BlockListItem(new GuidUdi(ContentBlock.ModelTypeAlias, Guid.NewGuid()), blockElement, null, null);

			var model = (ContentBlockViewModel)((ViewViewComponentResult)this.contentBlockViewComponent.Invoke(blockListItem)).ViewData.Model;

			Assert.AreEqual(bodyTextEncodedHtml, model.BodyText);
		}
	}
}
