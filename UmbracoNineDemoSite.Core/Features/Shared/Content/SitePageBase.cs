using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

using generatedModels = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
	public class SitePageBase
	{
		private readonly generatedModels.ISEO seoModel;

		public SitePageBase() { }
		public SitePageBase(IPublishedContent content)
		{
			SiteName = content?.Root()?.Name;

			seoModel = content as generatedModels.ISEO ?? new generatedModels.SEO(content, null);
			if (seoModel != null)
			{
				Id = seoModel.Id;
				Name = seoModel.Name;
				PageTitle = seoModel.PageTitle;
				PageDescription = seoModel.PageDescription;
			}
		}

		public string BodyClass = "frontpage theme-font-serif theme-color-earth";

		public int Id { get; set; }

		public string Name { get; set; }

		public virtual string PageTitle { get; set; }

		public virtual string PageDescription { get; set; }

		public string SiteName { get; set; }
	}
}