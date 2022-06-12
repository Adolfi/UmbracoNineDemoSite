using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
	public class SearchPageViewModel : SitePageBase, IHeadingPage
	{
		public SearchPageViewModel() : base() { }

		public string Heading { get; set; }
		public SearchFormModel SearchForm { get; set; }
	}
}
