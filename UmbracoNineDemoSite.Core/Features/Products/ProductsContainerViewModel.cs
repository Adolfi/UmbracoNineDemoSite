using System.Collections.Generic;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.Products
{
	public class ProductsContainerViewModel : SitePageBase, IHeadingPage
	{
		public ProductsContainerViewModel() : base() { }

		public string Heading { get; set; }

		public IEnumerable<ProductPageViewModel> Products { get; set; }
	}
}
