using System;
using System.Runtime.Serialization;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Content
{
	public class SitePageBase
	{
		private readonly gM.ISEO seoModel;
		public SitePageBase(IPublishedContent content)
		{
			SiteName = content.Root().Name;

			seoModel = content as gM.ISEO ?? new gM.SEO(content, null);
		}

		public string BodyClass = "frontpage theme-font-serif theme-color-earth";

		public int Id => seoModel.Id;

		public string Name => seoModel.Name;

		public virtual string PageTitle => seoModel.PageTitle;

		public virtual string PageDescription => seoModel.PageDescription;

		public string SiteName { get; }
	}
}