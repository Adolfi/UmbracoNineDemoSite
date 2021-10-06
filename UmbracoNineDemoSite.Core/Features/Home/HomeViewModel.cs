using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Hero;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Models;

namespace UmbracoNineDemoSite.Core.Features.Home
{
	public class HomeViewModel : SitePageBase, IHeadingPage
	{
		private readonly gM.Home homeModel;
		public HomeViewModel(IPublishedContent content) : base(content) {
			homeModel = content as gM.Home;
			if (homeModel == null) // necessary for test
			{
				homeModel = new gM.Home(content, null);
			}
		}

		public string Heading => homeModel.Heading;// this.Content.GetProperty(PropertyAlias.Heading).GetValue() as string;
		public string Preamble => homeModel.Preamble;// this.Content.GetProperty(PropertyAlias.Preamble).GetValue() as string;
		public string BackgroundImage => homeModel.BackgroundImage;// this.Content.GetProperty(PropertyAlias.BackgroundImage).GetValue() as string;
		public string CallToActionUrl => homeModel.CallToActionUrl?.Url();// (this.Content.GetProperty(PropertyAlias.CallToActionUrl).GetValue() as IPublishedContent)?.Url();
		public string CallToActionLabel => homeModel.CallToActionLabel;// this.Content.GetProperty(PropertyAlias.CallToActionLabel).GetValue() as string;
		public BlockListModel Blocks => homeModel.Blocks;// this.Content.GetProperty(PropertyAlias.Blocks).GetValue() as BlockListModel;
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
