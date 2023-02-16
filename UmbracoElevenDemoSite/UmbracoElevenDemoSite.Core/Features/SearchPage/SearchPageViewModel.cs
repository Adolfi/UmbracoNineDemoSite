using UmbracoElevenDemoSite.Core.Features.Shared.Content;

namespace UmbracoElevenDemoSite.Core.Features.SearchPage
{
	public class SearchPageViewModel : SitePageBase, IHeadingPage
	{
		public SearchPageViewModel() : base() { }

		public string Heading { get; set; }
		public SearchFormModel SearchForm { get; set; }
	}
}
