using Umbraco.Cms.Core.Models.Blocks;
using UmbracoTenDemoSite.Core.Features.Shared.Components.Hero;
using UmbracoTenDemoSite.Core.Features.Shared.Content;

namespace UmbracoTenDemoSite.Core.Features.Home
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
