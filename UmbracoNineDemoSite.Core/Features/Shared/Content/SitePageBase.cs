using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
	public class SitePageBase
	{
		private readonly gM.ISEO seoModel;

		public SitePageBase() { }
		public SitePageBase(IPublishedContent content)
		{
			SiteName = content?.Root()?.Name ;

			seoModel = content as gM.ISEO ?? new gM.SEO(content, null);
			if(seoModel != null)
			{
				Id = seoModel.Id;
				Name = seoModel.Name;
				PageTitle = seoModel.PageTitle;
				PageDescription = seoModel.PageDescription;
			}
		}

		public string BodyClass = "frontpage theme-font-serif theme-color-earth";

		public int Id { get; set; } //=> ;

		public string Name { get; set; } // => seoModel.Name;

		public virtual string PageTitle { get; set; } // => seoModel.PageTitle;

		public virtual string PageDescription { get; set; } //=> seoModel.PageDescription;

		public string SiteName { get; set;  }
	}
}