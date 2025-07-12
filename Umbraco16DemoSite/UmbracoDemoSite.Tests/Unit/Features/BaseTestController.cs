using Microsoft.AspNetCore.Mvc.ViewEngines;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;
using UmbracoDemoSite.Tests.Extensions;
using UmbracoDemoSite.Tests.Models;
using UmbracoDemoSite.Tests.Unit.Helper;

namespace UmbracoDemoSite.Tests.Unit.Features
{
    public class BaseTestController : BaseViewModelController
    {
        protected Mock<IPublishedContent> publishedContentMock = new();
        protected IPublishedContent publishedContent;
        protected Mock<IPublishedContent> callToActionUrlMock = new();

        protected ICompositeViewEngine compositeViewEngine = Mock.Of<ICompositeViewEngine>();

        protected IPublishedContentTypeCache publishedContentTypeCache = Mock.Of<IPublishedContentTypeCache>();

        protected ContentModel? contentModel;
        public BaseTestController()
        {
            var home = contentAndAccessor.Contents.FirstOrDefault(c => c.Id == 1000)
                ?? throw new InvalidOperationException("Home content not found.");

            publishedContentMock = Mock.Get(home);
            publishedContentMock.SetupPropertyValue("pageTitle", "PageTitle");
            publishedContentMock.SetupPropertyValue("pageDescription", "PageDescription");
            publishedContentMock.SetupPropertyValue("preamble", "Preamble");
            publishedContentMock.SetupPropertyValue("backgroundImage", "BackgroundImage");
            publishedContentMock.SetupPropertyValue("heading", "Heading");
            publishedContentMock.SetupPropertyValue("callToActionLabel", "CallToActionLabel");

            callToActionUrlMock = new Mock<IPublishedContent>();
            callToActionUrlMock.SetupPropertyValue("url", "https://example.com/call-to-action");
            publishedContentMock.SetupPropertyValue("callToActionUrl", callToActionUrlMock);

            publishedContentMock.SetupPropertyValue("blocks", new BlockListModel(new List<BlockListItem>()));

            publishedContent = publishedContentMock.Object;
        }
    }
}
