using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Hero;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Home
{
	public class HomeViewModel : SitePageBase, IHeadingPage
	{
		public HomeViewModel() : base() { }

		public string Heading { get; set; }
		public string Preamble { get; set; }
		public string BackgroundImage { get; set; }
		public string CallToActionUrl { get; set; }
		public string CallToActionLabel { get; set; }
		public BlockListModel Blocks { get; set; }
		public HeroViewModel Hero { get; set; }
	}
}
