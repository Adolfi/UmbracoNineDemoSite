using Moq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineDemoSite.Tests.Extensions
{
    public static class PublishedContentMockExtensions
    {
        public static void SetupPropertyValue(this Mock<IPublishedContent> publishedContentMock, string alias, object value, string culture = null, string segment = null)
        {
            var property = new Mock<IPublishedProperty>();
            property.Setup(x => x.Alias).Returns(alias);
            property.Setup(x => x.GetValue(culture, segment)).Returns(value);
            property.Setup(x => x.HasValue(culture, segment)).Returns(value != null);
            publishedContentMock.Setup(x => x.GetProperty(alias)).Returns(property.Object);
        }
    }
}
