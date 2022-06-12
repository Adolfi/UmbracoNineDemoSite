using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using UmbracoTenDemoSite.Core.Features.Shared.Components.ContactForm;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using Umbraco.Cms.Web.Website.ActionResults;
using System;

namespace UmbracoTenDemoSite.Tests.Unit.Features.Shared.Components.ContactForm
{
    [TestFixture]
    public class ContactFormControllerTests
    {
        private Mock<ITempDataDictionary> tempData;
        private Mock<IUmbracoContextAccessor> umbracoContextAccessor;
        private ContactFormController controller;

        [SetUp]
        public void SetUp()
        {
            this.tempData = new Mock<ITempDataDictionary>();
            this.umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();            
            this.controller = new ContactFormController(this.umbracoContextAccessor.Object, Mock.Of<IUmbracoDatabaseFactory>(), ServiceContext.CreatePartial(), AppCaches.NoCache, Mock.Of<IProfilingLogger>(), Mock.Of<IPublishedUrlProvider>())
            {
                TempData = this.tempData.Object
            };
        }

        [Test]
        public void Given_ContactFormModel_And_CurrentPageExists_When_Submit_Then_RedirectToCurrentPage()
        {
            var key = new Guid("11f0e7f5-4985-4264-a9ae-a8e04f2cd897");
            this.SetupCurrentPage(key);

            var result = (RedirectToUmbracoPageResult)controller.Submit(new Core.Features.Shared.Components.ContactForm.ContactForm());

            Assert.AreEqual(key, result.Key);
        }

        [Test]
        public void Given_ContactFormModel_And_CurrentPageExists_When_Submit_Then_ExpectTempData()
        {
            var key = new Guid("11f0e7f5-4985-4264-a9ae-a8e04f2cd897");
            this.SetupCurrentPage(key);

            var contactForm = new Core.Features.Shared.Components.ContactForm.ContactForm()
            {
                Name = "John Doe"
            };

            controller.Submit(contactForm);

            this.tempData.Verify(mock => mock.Add(TempDataKey.ResponseMessage, $"Thank you John Doe!"));
        }

        public void SetupCurrentPage(Guid key)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.Setup(content => content.Key).Returns(key);

            var publishedRequest = new Mock<IPublishedRequest>();
            publishedRequest.Setup(request => request.PublishedContent).Returns(publishedContent.Object);

            var umbracoContextMock = new Mock<IUmbracoContext>();
            umbracoContextMock.Setup(context => context.PublishedRequest).Returns(publishedRequest.Object);

            var umbracoContext = umbracoContextMock.Object;
            umbracoContextAccessor.Setup(x => x.TryGetUmbracoContext(out umbracoContext)).Returns(true);
        }
    }
}
