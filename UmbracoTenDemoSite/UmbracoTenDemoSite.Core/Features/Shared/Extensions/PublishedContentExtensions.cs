using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace UmbracoNineDemoSite.Core.Features.Shared.Extensions
{
    public static class PublishedContentExtensions
    {
        public static string MenuName(this IPublishedContent content) => content.Value<string>(nameof(SEO.PageTitle).ToFirstLower());
    }
}
