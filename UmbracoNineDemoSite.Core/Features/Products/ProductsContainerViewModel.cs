using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductsContainerViewModel : SitePageBase, IHeadingPage
    {
        public ProductsContainerViewModel(IPublishedContent content) : base(content) { }

        public string Heading => this.Content.GetProperty(PropertyAlias.Heading).GetValue() as string;

        public IEnumerable<ProductPageViewModel> Products { get; set; }
    }
}
