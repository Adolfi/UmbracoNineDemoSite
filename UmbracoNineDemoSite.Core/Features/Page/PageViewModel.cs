using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Page
{
	public class PageViewModel : SitePageBase, IHeadingPage
	{
		public PageViewModel() : base() { }

		public string Heading { get; set; }

		public IHtmlEncodedString BodyText { get; set; }

		public BlockListModel Blocks { get; set; }
	}
}
