using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.SearchPage
{
	public class SearchPageViewModel : SitePageBase, IHeadingPage
	{
		private readonly gM.SearchPage gModel;
		public SearchPageViewModel(IPublishedContent content) : base(content)
		{
			gModel = content as gM.SearchPage ?? new gM.SearchPage(content, null);

			SearchForm = new SearchFormModel
			{
				NoResultsFound = gModel.NoResultsFoundText,
				TotalResults = gModel.TotalResults,
				SearchTermText = gModel.SearchTermText
			};
		}

		public string Heading => gModel.Heading;

		public SearchFormModel SearchForm { get; set; }
	}
}
