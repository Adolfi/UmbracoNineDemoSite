using UmbracoDemoSite.Core.Features.Shared.Content;

namespace UmbracoDemoSite.Core.Features.SearchPage
{
	public class SearchPageViewModel : SitePageBase, IHeadingPage
	{
		public SearchPageViewModel() : base() { }

		public string? Heading { get; set; }
		public SearchFormModel? SearchForm { get; set; }
	}
}
