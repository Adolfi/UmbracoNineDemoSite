using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Products
{
	public class ProductsContainerViewModel : SitePageBase, IHeadingPage
	{
		private readonly gM.ProductsContainer gModel;
		public ProductsContainerViewModel(IPublishedContent content) : base(content)
		{
			gModel = content as gM.ProductsContainer ?? new gM.ProductsContainer(content, null);
		}

		public string Heading => gModel.Heading;

		public IEnumerable<ProductPageViewModel> Products { get; set; }
	}
}
