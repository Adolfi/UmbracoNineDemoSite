using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Header;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;

namespace UmbracoDemoSite.Tests.Unit.Features.Shared.Components.Header
{
    [TestFixture]
    public class HeaderViewComponentTests
    {
        private Mock<ISiteSettingsViewModel>? siteSettings;
        private HeaderViewComponent? headerViewComponent;

        [SetUp]
        public void SetUp()
        {
            siteSettings = new Mock<ISiteSettingsViewModel>();
            headerViewComponent = new HeaderViewComponent(siteSettings.Object);
        }

        [Test]
        [TestCase("siteName")]
        [TestCase("Umbraco 9 Demo")]
        public void Given_SiteSettingsHasSiteName_When_Invoke_Then_ReturnViewModelWithHeading(string siteName)
        {
            siteSettings!.Setup(x => x.SiteName).Returns(siteName);

            var model = (HeaderViewModel?)((ViewViewComponentResult)headerViewComponent!.Invoke(0))?.ViewData?.Model;

            Assert.That(siteName, Is.EqualTo(model?.Heading));
        }

        [Test]
        [TestCase(1)]
        [TestCase(12)]
        public void Given_SelectedParameter_When_Invoke_Then_ReturnViewModelWithSelectedValue(int selected)
        {
            var model = (HeaderViewModel?)((ViewViewComponentResult)headerViewComponent!.Invoke(selected))?.ViewData?.Model;

            Assert.That(selected, Is.EqualTo(model?.Selected));
        }
    }
}
