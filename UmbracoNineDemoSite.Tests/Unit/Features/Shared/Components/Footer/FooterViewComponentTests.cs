using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Footer;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Components.Footer
{
    [TestFixture]
    public class FooterViewComponentTests
    {
        private readonly Mock<ISiteSettings> siteSettings;
        private readonly FooterViewComponent footerViewComponent;

        public FooterViewComponentTests()
        {
            this.siteSettings = new Mock<ISiteSettings>();
            this.footerViewComponent = new FooterViewComponent(this.siteSettings.Object);
        }

        [Test]
        [TestCase("header")]
        [TestCase("heading")]        
        public void Given_SiteSettingsHasCallToActionHeader_When_Invoke_Then_ReturnViewModelWithCallToActionHeader(string expected)
        {
            this.siteSettings.Setup(x => x.CallToActionHeader).Returns(expected);

            var model = (FooterViewModel)((ViewViewComponentResult)this.footerViewComponent.Invoke()).ViewData.Model;

            Assert.AreEqual(expected, model.CallToActionHeader);
        }

        [Test]
        [TestCase("description")]
        [TestCase("descriptor")]
        public void Given_SiteSettingsHasCallToActionDescription_When_Invoke_Then_ReturnViewModelWithCallToActionDescription(string expected)
        {
            this.siteSettings.Setup(x => x.CallToActionDescription).Returns(expected);

            var model = (FooterViewModel)((ViewViewComponentResult)this.footerViewComponent.Invoke()).ViewData.Model;

            Assert.AreEqual(expected, model.CallToActionDescription);
        }

        [Test]
        [TestCase("/")]
        [TestCase("label")]
        public void Given_SiteSettingsHasCallToActionUrl_When_Invoke_Then_ReturnViewModelWithCallToActionUrl(string expected)
        {
            var content = new Mock<IPublishedContent>();
            content.Setup(x => x.Url(null, UrlMode.Default)).Returns(expected);
            this.siteSettings.Setup(x => x.CallToActionUrl).Returns(content.Object);

            var model = (FooterViewModel)((ViewViewComponentResult)this.footerViewComponent.Invoke()).ViewData.Model;

            Assert.AreEqual(expected, model.CallToActionUrl);
        }

        [Test]
        [TestCase("button")]
        [TestCase("label")]
        public void Given_SiteSettingsHasCallToActionButtonLabel_When_Invoke_Then_ReturnViewModelWithCallToActionButtonLabel(string expected)
        {
            this.siteSettings.Setup(x => x.CallToActionButtonLabel).Returns(expected);

            var model = (FooterViewModel)((ViewViewComponentResult)this.footerViewComponent.Invoke()).ViewData.Model;

            Assert.AreEqual(expected, model.CallToActionButtonLabel);
        }

        [Test]
        [TestCase("footer")]
        [TestCase("text")]
        public void Given_SiteSettingsHasFooterText_When_Invoke_Then_ReturnViewModelWithText(string expected)
        {
            this.siteSettings.Setup(x => x.FooterText).Returns(expected);

            var model = (FooterViewModel)((ViewViewComponentResult)this.footerViewComponent.Invoke()).ViewData.Model;

            Assert.AreEqual(expected, model.Text);
        }
    }
}
