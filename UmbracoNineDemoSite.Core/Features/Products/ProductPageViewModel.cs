using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Content;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductPageViewModel : SitePageBase, IHeadingPage
    {
        private readonly IPublishedContent container;

        public ProductPageViewModel(IProduct product, IPublishedContent container) : base(container)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.ImageUrl = product.ImageUrl;
            this.Price = product.Price;
            this.container = container;
        }

        public new int Id { get; set; }
        public new string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string UrlSegment => $"{Id}/{Name}";

        public string Heading => this.container.GetProperty(PropertyAlias.Heading).GetValue() as string;
        public override string PageTitle => this.Name;
    }
}
