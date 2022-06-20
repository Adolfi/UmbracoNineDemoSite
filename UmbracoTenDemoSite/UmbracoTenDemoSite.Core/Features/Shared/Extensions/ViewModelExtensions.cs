using Umbraco.Extensions;
using UmbracoTenDemoSite.Core.Features.Shared.Content;
using generatedModels = UmbracoTenDemoSite.Core;

namespace UmbracoTenDemoSite.Core.Features.Shared.Extensions
{
	internal static class ViewModelExtensions
	{
		public static void MapSitePageBase(this SitePageBase pageBase, generatedModels.ISEO currentModel)
		{
			pageBase.SiteName = currentModel?.Root()?.Name;
			pageBase.Id = currentModel?.Id ?? 0;
			pageBase.Name = currentModel?.Name;
			pageBase.PageTitle = currentModel?.PageTitle ?? currentModel?.Name;
			pageBase.PageDescription = currentModel?.PageDescription;

		}
	}
}
