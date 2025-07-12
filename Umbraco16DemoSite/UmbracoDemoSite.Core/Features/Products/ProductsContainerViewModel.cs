using System.Collections.Generic;
using UmbracoDemoSite.Core.Features.Shared.Content;

namespace UmbracoDemoSite.Core.Features.Products
{
	public class ProductsContainerViewModel : SitePageBase, IHeadingPage
	{
		public ProductsContainerViewModel() : base() { }

		public string? Heading { get; set; }

		public IEnumerable<ProductPageViewModel>? Products { get; set; }
	}
}
