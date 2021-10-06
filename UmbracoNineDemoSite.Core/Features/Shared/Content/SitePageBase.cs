using System.Runtime.Serialization;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

using gM = UmbracoNineDemoSite.Models;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
	public class SitePageBase : ContentModel
	{
		private readonly gM.ISEO seoModel;
		public SitePageBase(IPublishedContent content) : base(content)
		{
			seoModel = content as gM.ISEO;
			if (seoModel == null)// necessary for test
			{
				seoModel = new gM.SEO(content, null);
			}
		}

		public string BodyClass = "frontpage theme-font-serif theme-color-earth";

		public int Id => this.Content.Id;

		public string Name => this.Content.Name;

		public virtual string PageTitle => seoModel.PageTitle;// this.Content.Value<string>(PropertyAlias.PageTitle);

		public virtual string PageDescription => seoModel.PageDescription;

		public string SiteName => this.Content.AncestorOrSelf(1).Name;
	}
}