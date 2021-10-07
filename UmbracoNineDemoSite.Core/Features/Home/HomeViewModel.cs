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
		private readonly gM.Home gModel;
		public HomeViewModel(IPublishedContent content) : base(content) {
			gModel = content as gM.Home ?? new gM.Home(content, null);
		}

		public string Heading => gModel.Heading;
		public string Preamble => gModel.Preamble;
		public string BackgroundImage => gModel.BackgroundImage;
		public string CallToActionUrl => gModel.CallToActionUrl?.Url();
		public string CallToActionLabel => gModel.CallToActionLabel;
		public BlockListModel Blocks => gModel.Blocks;
		public HeroViewModel Hero => new()
		{
			Heading = this.Heading,
			Preamble = this.Preamble,
			BackgroundImageUrl = this.BackgroundImage,
			CallToActionUrl = this.CallToActionUrl,
			CallToActionLabel = this.CallToActionLabel
		};
	}
}
