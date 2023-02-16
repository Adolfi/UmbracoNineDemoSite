using System.Collections.Generic;
using UmbracoElevenDemoSite.Core.Features.Shared.Content;

namespace UmbracoElevenDemoSite.Core.Features.Products
{
	public class ProductsContainerViewModel : SitePageBase, IHeadingPage
	{
		public ProductsContainerViewModel() : base() { }

		public string Heading { get; set; }

		public IEnumerable<ProductPageViewModel> Products { get; set; }
	}
}
