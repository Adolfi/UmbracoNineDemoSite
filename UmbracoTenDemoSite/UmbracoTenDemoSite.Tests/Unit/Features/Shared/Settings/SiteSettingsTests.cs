using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using UmbracoTenDemoSite.Core.Features.Shared.Settings;
using UmbracoTenDemoSite.Tests.Extensions;
using generatedModels = UmbracoTenDemoSite.Core;

namespace UmbracoTenDemoSite.Tests.Unit.Features.Shared.Settings
{
    [TestFixture]
    public class SiteSettingsTests
    {
        private delegate void ServiceTryGetUmbracoContext(out IUmbracoContext context);

        private Mock<IPublishedContentCache> contentCache;

        private Mock<generatedModels.SiteSettings> siteSettings;
        private Mock<generatedModels.Home> homeContent;
        private SiteSettings siteSettingsViewModel;
        private Mock<IUmbracoContextAccessor> umbracoContextAccessor;

        [SetUp]
        public void SetUp()
        {
            var productsContainerContent = Mock.Of<IPublishedContent>();
            var productsContainerFallback = Mock.Of<IPublishedValueFallback>();

            homeContent = new Mock<generatedModels.Home>(
                productsContainerContent, productsContainerFallback);

            siteSettings = new Mock<generatedModels.SiteSettings>(
                productsContainerContent, productsContainerFallback);
            var contentType = new Mock<IPublishedContentType>();
            contentType.Setup(s => s.Alias).Returns(generatedModels.SiteSettings.ModelTypeAlias);
            siteSettings.Setup(s => s.ContentType).Returns(contentType.Object);

            homeContent.Setup(s => s.Children)
                .Returns(new IPublishedContent[] { siteSettings.Object });

            contentCache = new Mock<IPublishedContentCache>();
            contentCache.Setup(s => s.GetAtRoot(null))
                .Returns(new IPublishedContent[] { homeContent.Object });

            var umbracoContext = new Mock<IUmbracoContext>();
            umbracoContext.Setup(s => s.Content)
                .Returns(contentCache.Object);

            IUmbracoContext ctx;
            umbracoContextAccessor = new Mock<IUmbracoContextAccessor>();
            umbracoContextAccessor
               .Setup(x => x.TryGetUmbracoContext(out ctx))
               .Callback(new ServiceTryGetUmbracoContext((out IUmbracoContext uContext) =>
               {
                   uContext = umbracoContext.Object;
               }));
        }

        [Test]
        [TestCase("Site name")]
        [TestCase("Umbraco 9 Demo")]
        public void Given_HomeNodeExistsAtXPath_When_GetSiteName_Then_ReturnExpectedSiteNameFromHomeNode(string siteName)
        {
            homeContent.Setup(s => s.Name).Returns(siteName);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.SiteName;

            Assert.AreEqual(siteName, result);
        }
        [Test]
        [TestCase("Call To Action Header")]
        [TestCase("Welcome to the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionHeader_When_GetCallToActionHeader_Then_ReturnExpectedCallToActionHeader(string callToActionHeader)
        {
            siteSettings.Setup(s => s.CallToActionHeader).Returns(callToActionHeader);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.CallToActionHeader;

            Assert.AreEqual(callToActionHeader, result);
        }

       
        [Test]
        [TestCase("Call To Action Description")]
        [TestCase("Description for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionDescription_When_GetCallToActionDescription_Then_ReturnExpectedCallToActionDescription(string callToActionDescription)
        {
            siteSettings.Setup(s => s.CallToActionDescription).Returns(callToActionDescription);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.CallToActionDescription;

            Assert.AreEqual(callToActionDescription, result);
        }

        [Test]
        public void Given_SettingsNodeHasCallToActionUrl_When_GetCallToActionUrl_Then_ReturnExpectedCallToActionUrl()
        {
            var callToActionContentReference = Mock.Of<IPublishedContent>();
            siteSettings.Setup(s => s.CallToActionUrl).Returns(callToActionContentReference);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.CallToActionUrl;

            Assert.AreEqual(callToActionContentReference, result);
        }

        [Test]
        [TestCase("Call To Action Label")]
        [TestCase("Label for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionButtonLabel_When_GetCallToActionButtonLabel_Then_ReturnExpectedCallToActionButtonLabel(string callToActionButtonLabel)
        {
            siteSettings.Setup(s => s.CallToActionButtonLabel).Returns(callToActionButtonLabel);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.CallToActionButtonLabel;

            Assert.AreEqual(callToActionButtonLabel, result);
        }
        [Test]
        [TestCase("Footer Text")]
        [TestCase("Footer Text for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasFooterText_When_GetFooterText_Then_ReturnExpectedFooterText(string footerText)
        {
            siteSettings.Setup(s => s.FooterText).Returns(footerText);

            siteSettingsViewModel = new SiteSettings(umbracoContextAccessor.Object);
            var result = this.siteSettingsViewModel.FooterText;

            Assert.AreEqual(footerText, result);
        }
    }
}
