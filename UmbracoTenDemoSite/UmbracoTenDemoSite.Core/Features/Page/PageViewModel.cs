using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using UmbracoTenDemoSite.Core.Features.Shared.Content;

namespace UmbracoTenDemoSite.Core.Features.Page
{
	public class PageViewModel : SitePageBase, IHeadingPage
	{
		public PageViewModel() : base() { }

		public string Heading { get; set; }

		public IHtmlEncodedString BodyText { get; set; }

		public BlockListModel Blocks { get; set; }
	}
}
