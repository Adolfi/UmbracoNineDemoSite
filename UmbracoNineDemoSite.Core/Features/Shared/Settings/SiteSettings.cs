using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Settings
{
	/// <summary>
	/// Exposing simple POCO properties which are set during initialization in the constructor (ctor).
	/// This allows a dependency injection scope of scoped (see: SiteSettingsComposer).
	/// Thus the setup happend only once per page request, although SiteSettings are injected in several ViewComponents.
	/// </summary>
	public class SiteSettings : ISiteSettings
	{
		private readonly UmbracoHelper umbracoHelper;

		public SiteSettings(UmbracoHelper umbracoHelper)
		{
			this.umbracoHelper = umbracoHelper;
			generatedModels.Home homeContent = umbracoHelper.ContentAtRoot().FirstOrDefault() as generatedModels.Home;
			if (homeContent == null) return;

			generatedModels.SiteSettings settings = homeContent.FirstChild<generatedModels.SiteSettings>();

			SiteName = homeContent.Name;
			CallToActionDescription = settings.CallToActionDescription;
			CallToActionButtonLabel = settings.CallToActionButtonLabel;
			CallToActionHeader = settings.CallToActionHeader;
			CallToActionUrl = settings.CallToActionUrl;
			FooterText = settings.FooterText;
		}
		public string SiteName { get; set; }
		public string CallToActionHeader { get; set; }
		public string CallToActionDescription { get; set; }
		public IPublishedContent CallToActionUrl { get; set; }
		public string CallToActionButtonLabel { get; set; }
		public string FooterText { get; set; }
	}
}
