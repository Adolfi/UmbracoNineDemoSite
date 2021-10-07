using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Shared.Extensions
{
	internal static class ViewModelExtensions
	{
		public static void MapSitePageBase(this SitePageBase pageBase, gM.ISEO currentModel)
		{
			pageBase.SiteName = currentModel?.Root()?.Name;
			pageBase.Id = currentModel?.Id ?? 0;
			pageBase.Name = currentModel?.Name;
			pageBase.PageTitle = currentModel?.PageTitle ?? currentModel?.Name;
			pageBase.PageDescription = currentModel?.PageDescription;

		}
	}
}
