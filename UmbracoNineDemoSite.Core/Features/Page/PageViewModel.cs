using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Page
{
	public class PageViewModel : SitePageBase, IHeadingPage
	{
		private readonly gM.Page gModel;
		public PageViewModel(IPublishedContent content) : base(content)
		{
			gModel = content as gM.Page ?? new gM.Page(content, null);
		}

		public string Heading => gModel.Heading;

		public IHtmlEncodedString BodyText => gModel.BodyText;

		public BlockListModel Blocks => gModel.Blocks;
	}
}
