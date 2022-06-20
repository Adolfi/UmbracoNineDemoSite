using Umbraco.Cms.Core.Models.PublishedContent;
using UmbracoTenDemoSite.Core.Features.Shared.Constants;
using UmbracoTenDemoSite.Core.Features.Shared.Content;
using UmbracoTenDemoSite.Integrations.Products.Entities;

namespace UmbracoTenDemoSite.Core.Features.Products
{
    public class ProductPageViewModel : SitePageBase, IHeadingPage
    {

        public ProductPageViewModel(IProduct product) : base()
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.PageDescription = product.ShortDescription;
            this.ImageUrl = product.ImageUrl;
            this.Price = product.Price;
        }

        public new int Id { get; set; }
        public new string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string UrlSegment => $"{Id}/{Name}";

        public string Heading { get; set; }
        public override string PageTitle => this.Name;
		public override string PageDescription { get; set; }
    }
}
