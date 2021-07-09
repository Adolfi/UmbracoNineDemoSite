using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
    public class SearchPageViewModel : SitePageBase, IHeadingPage
    {
        public SearchPageViewModel(IPublishedContent content) : base(content)
        {
            SearchForm = new SearchFormModel
            {
                NoResultsFound = this.Content.Value<string>(PropertyAlias.NoResultsFoundText),
                TotalResults = this.Content.Value<string>(PropertyAlias.TotalResults),
                SearchTermText = this.Content.Value<string>(PropertyAlias.SearchTermText)
            };
        }

        public string Heading => this.Content.Value<string>(PropertyAlias.Heading);

        public SearchFormModel SearchForm { get; set; }
    }
}
