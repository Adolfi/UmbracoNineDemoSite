using System;
using System.Linq;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Integrations.Products.Services;
//using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Core.Features.Products
{
	/// <summary>
	/// Docs: https://our.umbraco.com/Documentation/Reference/Routing/Request-Pipeline/IContentFinder
	/// </summary>
	public class ProductsContentFinder : IContentFinder
	{
		private readonly IProductService productService;
		private readonly IUmbracoContextAccessor umbracoContextAccessor;
		public readonly IPublishedSnapshotAccessor publishedSnapshotAccessor;

		public ProductsContentFinder(IProductService productService, IUmbracoContextAccessor umbracoContextAccessor, IPublishedSnapshotAccessor publishedSnapshotAccessor)
		{
			this.productService = productService;
			this.umbracoContextAccessor = umbracoContextAccessor;
			this.publishedSnapshotAccessor = publishedSnapshotAccessor;
		}

		public bool TryFindContent(IPublishedRequestBuilder request)
		{
			var segments = request.AbsolutePathDecoded.Split("/");
			if (!int.TryParse(segments[2], out var id))
			{
				return false;
			}

			var product = this.productService.Get(id);
			if (product == null)
			{
				return false;
			}

			umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);
			var container = umbracoContext?.Content.GetByContentType(ProductsContainer.GetModelContentType(publishedSnapshotAccessor))?.FirstOrDefault();

			if (container == null)
			{
				return false;
			}

			request.SetPublishedContent(container);
			return true;
		}
	}
}
