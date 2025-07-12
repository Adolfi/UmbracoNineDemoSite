using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemoSite.Core.Features.Shared.Components.Footer;
using UmbracoDemoSite.Core.Features.Shared.Settings;

namespace UmbracoDemoSite.Tests.Unit.Features.Shared.Components.Footer
{
    [TestFixture]
    public class FooterViewComponentTests
    {
        private Mock<ISiteSettingsViewModel>? siteSettings;
        private FooterViewComponent? footerViewComponent;

        [SetUp]
        public void SetUp()
        {
            siteSettings = new Mock<ISiteSettingsViewModel>();
            footerViewComponent = new FooterViewComponent(siteSettings.Object);
        }

        [Test]
        [TestCase("header")]
        [TestCase("heading")]        
        public void Given_SiteSettingsHasCallToActionHeader_When_Invoke_Then_ReturnViewModelWithCallToActionHeader(string expected)
        {
            siteSettings!.Setup(x => x.CallToActionHeader).Returns(expected);

            var model = (FooterViewModel?)((ViewViewComponentResult)footerViewComponent!.Invoke())?.ViewData?.Model;

            Assert.That(expected, Is.EqualTo(model?.CallToActionHeader));
        }

        [Test]
        [TestCase("description")]
        [TestCase("descriptor")]
        public void Given_SiteSettingsHasCallToActionDescription_When_Invoke_Then_ReturnViewModelWithCallToActionDescription(string expected)
        {
            siteSettings!.Setup(x => x.CallToActionDescription).Returns(expected);

            var model = (FooterViewModel?)((ViewViewComponentResult)footerViewComponent!.Invoke())?.ViewData?.Model;

            Assert.That(expected, Is.EqualTo(model?.CallToActionDescription));
        }
         
        [Test]
        [TestCase("button")]
        [TestCase("label")]
        public void Given_SiteSettingsHasCallToActionButtonLabel_When_Invoke_Then_ReturnViewModelWithCallToActionButtonLabel(string expected)
        {
            siteSettings!.Setup(x => x.CallToActionButtonLabel).Returns(expected);

            var model = (FooterViewModel?)((ViewViewComponentResult)footerViewComponent!.Invoke())?.ViewData?.Model;

            Assert.That(expected, Is.EqualTo(model?.CallToActionButtonLabel));
        }

        [Test]
        [TestCase("footer")]
        [TestCase("text")]
        public void Given_SiteSettingsHasFooterText_When_Invoke_Then_ReturnViewModelWithText(string expected)
        {
            siteSettings!.Setup(x => x.FooterText).Returns(expected);

            var model = (FooterViewModel?)((ViewViewComponentResult)footerViewComponent!.Invoke())?.ViewData?.Model;

            Assert.That(expected, Is.EqualTo(model?.Text));
        }
    }
}
