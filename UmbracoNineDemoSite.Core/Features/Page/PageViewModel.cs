using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Page
{
	public class PageViewModel : SitePageBase, IHeadingPage
	{
		//private readonly gM.Page gModel;
		//public PageViewModel(IPublishedContent content) : base(content)
		//{
		//	gModel = content as gM.Page ?? new gM.Page(content, null);
		//}

		public PageViewModel() : base() { }

		public string Heading { get; set; } //=> gModel.Heading;

		public IHtmlEncodedString BodyText { get; set; } //=> gModel.BodyText;

		public BlockListModel Blocks { get; set; } //=> gModel.Blocks;
	}
}
