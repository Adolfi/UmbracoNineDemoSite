using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
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
        public SiteSettings(IUmbracoContextAccessor umbracoContextAccessor)
		{
			umbracoContextAccessor
				.TryGetUmbracoContext(out IUmbracoContext umbracoContext);
            if (umbracoContext.Content.GetAtRoot().FirstOrDefault() is not generatedModels.Home homeContent) return;

			if (homeContent.Children.FirstOrDefault(c => c.ContentType.Alias.Equals(generatedModels.SiteSettings.ModelTypeAlias)) is not generatedModels.SiteSettings settings) return;

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
