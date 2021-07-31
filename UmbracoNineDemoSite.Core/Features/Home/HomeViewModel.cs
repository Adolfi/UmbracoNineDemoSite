using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Components.Hero;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.Home
{
	public class HomeViewModel : SitePageBase, IHeadingPage
	{
		public HomeViewModel(IPublishedContent content) : base(content) { }

		public string Heading => this.Content.GetProperty(PropertyAlias.Heading).GetValue() as string;
		public string Preamble => this.Content.GetProperty(PropertyAlias.Preamble).GetValue() as string;
		public string BackgroundImage => this.Content.GetProperty(PropertyAlias.BackgroundImage).GetValue() as string;
		public string CallToActionUrl => (this.Content.GetProperty(PropertyAlias.CallToActionUrl).GetValue() as IPublishedContent)?.Url();
		public string CallToActionLabel => this.Content.GetProperty(PropertyAlias.CallToActionLabel).GetValue() as string;
		public BlockListModel Blocks => this.Content.GetProperty(PropertyAlias.Blocks).GetValue() as BlockListModel;
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
