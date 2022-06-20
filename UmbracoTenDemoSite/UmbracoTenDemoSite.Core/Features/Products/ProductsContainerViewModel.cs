using System.Collections.Generic;
using UmbracoTenDemoSite.Core.Features.Shared.Content;

namespace UmbracoTenDemoSite.Core.Features.Products
{
	public class ProductsContainerViewModel : SitePageBase, IHeadingPage
	{
		public ProductsContainerViewModel() : base() { }

		public string Heading { get; set; }

		public IEnumerable<ProductPageViewModel> Products { get; set; }
	}
}
