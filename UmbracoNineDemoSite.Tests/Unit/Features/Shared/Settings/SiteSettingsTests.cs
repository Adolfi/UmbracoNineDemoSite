﻿using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Web.Common;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;
using UmbracoNineDemoSite.Tests.Extensions;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Settings
{
    [TestFixture]
    public class SiteSettingsTests
    {
        private Mock<ICultureDictionaryFactory> cultureDictionaryFactory;
        private Mock<IUmbracoComponentRenderer> componentRenderer;
        private Mock<IPublishedContentQuery> publishedContentQuery;

        private UmbracoHelper umbracoHelper;
        private SiteSettings siteSettings;

        [SetUp]
        public void SetUp()
        {
            this.cultureDictionaryFactory = new Mock<ICultureDictionaryFactory>();
            this.componentRenderer = new Mock<IUmbracoComponentRenderer>();
            this.publishedContentQuery = new Mock<IPublishedContentQuery>();
            
            this.umbracoHelper = new UmbracoHelper(this.cultureDictionaryFactory.Object, this.componentRenderer.Object, this.publishedContentQuery.Object);
            this.siteSettings = new SiteSettings(this.umbracoHelper);
        }

        [Test]
        [TestCase("Site name")]
        [TestCase("Umbraco 9 Demo")]
        public void Given_HomeNodeExistsAtXPath_When_GetSiteName_Then_ReturnExpectedSiteNameFromHomeNode(string siteName)
        {
            this.MockSettings(new Mock<IPublishedContent>(), siteName);

            var result = this.siteSettings.SiteName;

            Assert.AreEqual(siteName, result);
        }

        [Test]
        [TestCase("Call To Action Header")]
        [TestCase("Welcome to the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionHeader_When_GetCallToActionHeader_Then_ReturnExpectedCallToActionHeader(string callToActionHeader)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionHeader, callToActionHeader);
            this.MockSettings(settingsNode);

            var result = this.siteSettings.CallToActionHeader;

            Assert.AreEqual(callToActionHeader, result);
        }

        [Test]
        [TestCase("Call To Action Description")]
        [TestCase("Description for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionDescription_When_GetCallToActionDescription_Then_ReturnExpectedCallToActionDescription(string callToActionDescription)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionDescription, callToActionDescription);
            this.MockSettings(settingsNode);

            var result = this.siteSettings.CallToActionDescription;

            Assert.AreEqual(callToActionDescription, result);
        }

        [Test]
        public void Given_SettingsNodeHasCallToActionUrl_When_GetCallToActionUrl_Then_ReturnExpectedCallToActionUrl()
        {
            var callToActionContentReference = Mock.Of<IPublishedContent>();

            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionUrl, callToActionContentReference);
            this.MockSettings(settingsNode);

            var result = this.siteSettings.CallToActionUrl;

            Assert.AreEqual(callToActionContentReference, result);
        }

        [Test]
        [TestCase("Call To Action Label")]
        [TestCase("Label for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionButtonLabel_When_GetCallToActionButtonLabel_Then_ReturnExpectedCallToActionButtonLabel(string callToActionButtonLabel)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionButtonLabel, callToActionButtonLabel);
            this.MockSettings(settingsNode);

            var result = this.siteSettings.CallToActionButtonLabel;

            Assert.AreEqual(callToActionButtonLabel, result);
        }

        [Test]
        [TestCase("Footer Text")]
        [TestCase("Footer Text for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasFooterText_When_GetFooterText_Then_ReturnExpectedFooterText(string footerText)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.FooterText, footerText);
            this.MockSettings(settingsNode);

            var result = this.siteSettings.FooterText;

            Assert.AreEqual(footerText, result);
        }

        private void MockSettings(Mock<IPublishedContent> settings, string siteName = "defaultSiteName")
        {
            var homeContentType = new Mock<IPublishedContentType>();
            homeContentType.Setup(homeType => homeType.Alias).Returns(ContentTypeAlias.Home);

            var homeNode = new Mock<IPublishedContent>();
            homeNode.Setup(home => home.Name).Returns(siteName);
            homeNode.Setup(home => home.ContentType).Returns(homeContentType.Object);

            var settingsContentType = new Mock<IPublishedContentType>();
            settingsContentType.Setup(homeType => homeType.Alias).Returns(ContentTypeAlias.SiteSettings);
            settings.Setup(home => home.ContentType).Returns(settingsContentType.Object);
            homeNode.Setup(home => home.Children).Returns(new List<IPublishedContent>() { settings.Object });

            this.publishedContentQuery.Setup(query => query.ContentAtRoot()).Returns(new List<IPublishedContent>() { homeNode.Object });
        }
    }
}
