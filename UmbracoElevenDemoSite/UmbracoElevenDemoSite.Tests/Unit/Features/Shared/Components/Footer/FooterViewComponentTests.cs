using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoElevenDemoSite.Core.Features.Shared.Components.Footer;
using UmbracoElevenDemoSite.Core.Features.Shared.Settings;

namespace UmbracoElevenDemoSite.Tests.Unit.Features.Shared.Components.Footer
{
    [TestFixture]
    public class FooterViewComponentTests
    {
        private Mock<ISiteSettings> siteSettings;
        private FooterViewComponent footerViewComponent;

        [SetUp]
        public void SetUp()
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

        // TODO: Add test for CallToActionUrl. Just have to figure out how to mock the Url() extension method in v9.
    }
}
