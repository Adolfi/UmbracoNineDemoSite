using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemoSite.Core.Features.Shared.Settings;
using Gm = UmbracoDemoSite.Core;

namespace UmbracoDemoSite.Tests.Unit.Features.Shared.Settings
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class SiteSettingsTests : BaseViewModelController
    {
        private delegate void ServiceTryConvertTo<T>(out T? value);

        private Mock<IPublishedContent>? settingsContentMock;
        private SiteSettingsViewModel? siteSettingsViewModel;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        [TestCase("Home")]
        [TestCase("Umbraco 16 Demo")]
        public void Given_HomeModelExists_When_GetSiteName_Then_ReturnExpectedSiteNameFromHomeNode(string sitename)
        {
            var homeContent = Mock.Of<IPublishedContent>(c => c.Name == sitename);
            rootMock = Mock.Get(homeContent);

            settingsContentMock = new();
            siteSettingsViewModel = UpdateViewModel();

            var result = siteSettingsViewModel?.SiteName;

            Assert.That(sitename, Is.EqualTo(result));
        }

        [Test]
        [TestCase("Call To Action Header")]
        [TestCase("Welcome to the Umbraco 16 Demo")]
        public void Given_SettingsNodeHasCallToActionHeader_When_GetCallToActionHeader_Then_ReturnExpectedCallToActionHeader(string callToActionHeader)
        {
            SetupTestProperty(callToActionHeader, "callToActionHeader");

            var result = siteSettingsViewModel?.CallToActionHeader;

            Assert.That(callToActionHeader, Is.EqualTo(result));
        }


        [Test]
        [TestCase("Call To Action Description")]
        [TestCase("Description for the Umbraco 16 Demo")]
        public void Given_SettingsNodeHasCallToActionDescription_When_GetCallToActionDescription_Then_ReturnExpectedCallToActionDescription(string callToActionDescription)
        {
            SetupTestProperty(callToActionDescription, "callToActionDescription");

            var result = siteSettingsViewModel?.CallToActionDescription;

            Assert.That(callToActionDescription, Is.EqualTo(result));
        }

        [Test]
        [TestCase("Call To Action Label")]
        [TestCase("Label for the Umbraco 16 Demo")]
        public void Given_SettingsNodeHasCallToActionButtonLabel_When_GetCallToActionButtonLabel_Then_ReturnExpectedCallToActionButtonLabel(string callToActionButtonLabel)
        {
            SetupTestProperty(callToActionButtonLabel, "callToActionButtonLabel");

            var result = siteSettingsViewModel?.CallToActionButtonLabel;

            Assert.That(callToActionButtonLabel, Is.EqualTo(result));
        }
        [Test]
        [TestCase("Footer Text")]
        [TestCase("Footer Text for the Umbraco 16 Demo")]
        public void Given_SettingsNodeHasFooterText_When_GetFooterText_Then_ReturnExpectedFooterText(string footerText)
        {
            SetupTestProperty(footerText, "footerText");

            var result = siteSettingsViewModel?.FooterText;

            Assert.That(footerText, Is.EqualTo(result));
        }


        // Umbraco.Extensions.ObjectExtensions: public static Attempt<T> TryConvertTo<T>(this object? input)
        // CANNOT BE MOCKED!

        //[Test]
        //public void Given_SettingsNodeHasCallToActionUrl_When_GetCallToActionUrl_Then_ReturnExpectedCallToActionUrl()
        //{
        //    var callToActionContentReference = new Mock<IPublishedContent>();
        //    callToActionContentReference
        //        .Setup(c => c.Name)
        //        .Returns("About us");
        //    callToActionContentReference
        //        .Setup(c => c.Id)
        //        .Returns(1001);
        //    SetupTestProperty(callToActionContentReference.Object, "callToActionUrl");

        //    var result = siteSettingsViewModel?.CallToActionUrl;

        //    Assert.That(callToActionContentReference, Is.EqualTo(result));
        //}

        private SiteSettingsViewModel UpdateViewModel() => new(settingsContentMock!.Object, viewModelService, fallback);
        private void SetupTestProperty(object valueObject, string propertyAlias)
        {
            var isIPublishedContent = valueObject is Mock<IPublishedContent>;
            var propertyMock = new Mock<IPublishedProperty>();
            propertyMock
                .Setup(p => p.HasValue(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns((string culture, string segment) =>
                {
                    return true;
                });
            propertyMock
                .Setup(p => p.GetValue(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns((string culture, string segment) =>
                {
                    var content = valueObject as IPublishedContent;
                    return isIPublishedContent ? content : valueObject.ToString();
                });
            // // Umbraco.Extensions.ObjectExtensions: public static Attempt<T> TryConvertTo<T>(this object? input)
            // // CANNOT BE MOCKED!
            // // Thus the following does not work:
            //propertyMock
            //    .Setup(p => p.TryConvertTo<object>())
            //    .Returns(new ServiceTryConvertTo<object>((out object? convertedValue) =>
            //    {
            //        convertedValue = propertyMock.Object;
            //    }));
            settingsContentMock = new();
            settingsContentMock
                .Setup(s => s.GetProperty(It.IsAny<string>()))
                .Returns((string alias) =>
                {
                    if (propertyAlias != alias)
                    { return null; }

                    return propertyMock.Object;
                });

            siteSettingsViewModel = UpdateViewModel();
        }
    }
}
