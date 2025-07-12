using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.ActionResults;
using UmbracoNineDemoSite.Core.Features.Shared.Components.ContactForm;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Components.ContactForm
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class ContactFormControllerTests
    {
        private Mock<ITempDataDictionary>? tempData;
        private Mock<IUmbracoContextAccessor>? umbracoContextAccessor;
        private ContactFormController? controller;

        [SetUp]
        public void SetUp()
        {
            tempData = new Mock<ITempDataDictionary>();
            umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();

            controller = new ContactFormController(
                umbracoContextAccessor.Object,
                Mock.Of<IUmbracoDatabaseFactory>(),
                ServiceContext.CreatePartial(),
                AppCaches.NoCache,
                Mock.Of<IProfilingLogger>(),
                Mock.Of<IPublishedUrlProvider>())
            {
                TempData = tempData.Object
            };
        }

        [Test]
        public void Given_ContactFormModel_And_CurrentPageExists_When_Submit_Then_RedirectToCurrentPage()
        {
            var key = new Guid("11f0e7f5-4985-4264-a9ae-a8e04f2cd897");
            this.SetupCurrentPage(key);

            var result = (RedirectToUmbracoPageResult)controller!.Submit(new ContactFormModel());

            Assert.That(key, Is.EqualTo(result.Key));
        }

        //[Test]
        //[TestCase("John Doe", "jd@example.com", "This is a test comment.", true)]
        //[TestCase("John Doe", "jd", "This is a test comment.", false)]
        //[TestCase("John Doe", "jd@example.com", "", false)]
        //public void Given_ContactFormModel_And_CurrentPageExists_When_Submit_Then_ExpectTempData(string name, string email, string comment, bool valid)
        //{
        //    var key = new Guid("11f0e7f5-4985-4264-a9ae-a8e04f2cd897");
        //    this.SetupCurrentPage(key);

        //    var contactForm = new ContactFormModel()
        //    {
        //        Name = name,
        //        Email = email,
        //        Comment = comment
        //    };

        //    controller!.Submit(contactForm);

        //    if (valid)
        //    {
        //        Assert.That(controller.ModelState.IsValid, Is.True);
        //        this.tempData!.Verify(mock => mock.Add(TempDataKey.ResponseMessage, $"Thank you John Doe!"));
        //    }
        //    else
        //    {
        //        Assert.That(controller.ModelState.IsValid, Is.False);
        //    }
        //}
        //[Test]
        //[TestCase("John Doe", "jd@example.com", "This is a test comment.", true)]
        //[TestCase("Lisa Doe", "Lisa.Doe@example.com", "A test comment.", true)]
        //[TestCase("Lisa Doe", "ld", "Test comment.", false)]
        //[TestCase("John Doe", "jd@example.com", "", false)]
        //public void Submit_BindsModel_FromPrefixedFormData(string name, string email, string comment, bool expectValid)
        //{
        //    // Arrange
        //    var formData = new Dictionary<string, StringValues>
        //    {
        //        { "formModel.Name", name },
        //        { "formModel.Email", email },
        //        { "formModel.Comment", comment }
        //    };

        //    var httpContext = new DefaultHttpContext();
        //    httpContext.Request.Form = new FormCollection(formData);

        //    controller!.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = httpContext
        //    };

        //    // Act
        //    var result = controller.Submit(new ContactFormModel());

        //    // Assert
        //    // You can check ModelState, TempData, or the result type
        //    Assert.That(controller.ModelState.IsValid, Is.EqualTo(expectValid));
        //    if (expectValid)
        //    {
        //        this.tempData!.Verify(mock => mock.Add(TempDataKey.ResponseMessage, $"Thank you {name}!"));
        //    }
        //}
        //[Test]
        //public void Submit_BindsModel_FromPrefixedFormData()
        //{
        //    var name = "John Doe";
        //    // Arrange
        //    var formData = new Dictionary<string, StringValues>
        //    {
        //        { "formModel.Name", name },
        //        { "formModel.Email", "john@example.com" },
        //        { "formModel.Comment", "Hello!" }
        //    };

        //    var httpContext = new DefaultHttpContext();
        //    httpContext.Request.Form = new FormCollection(formData);

        //    var controller = new ContactFormController(
        //        umbracoContextAccessor!.Object,
        //        Mock.Of<IUmbracoDatabaseFactory>(),
        //        ServiceContext.CreatePartial(),
        //        AppCaches.NoCache,
        //        Mock.Of<IProfilingLogger>(),
        //        Mock.Of<IPublishedUrlProvider>())
        //    {
        //        TempData = tempData!.Object
        //    };
        //    controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = httpContext
        //    };

        //    // Act
        //    var model = new ContactFormModel()
        //    {
        //        Name = formData["formModel.Name"],
        //        Email = formData["formModel.Email"],
        //        Comment = formData["formModel.Comment"]
        //    };
        //    var result = controller.Submit(model);

        //    // Assert
        //    // You can check ModelState, TempData, or the result type
        //    Assert.That(controller.ModelState.IsValid, Is.EqualTo(true));
        //    this.tempData!.Verify(mock => mock.Add(TempDataKey.ResponseMessage, $"Thank you {name}!"));
        //}

        public void SetupCurrentPage(Guid key)
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.Setup(content => content.Key).Returns(key);

            var publishedRequest = new Mock<IPublishedRequest>();
            publishedRequest.Setup(request => request.PublishedContent).Returns(publishedContent.Object);

            var umbracoContextMock = new Mock<IUmbracoContext>();
            umbracoContextMock.Setup(context => context.PublishedRequest).Returns(publishedRequest.Object);

            var umbracoContext = umbracoContextMock.Object;
            umbracoContextAccessor!.Setup(x => x.TryGetUmbracoContext(out umbracoContext)).Returns(true);
        }
    }
}
