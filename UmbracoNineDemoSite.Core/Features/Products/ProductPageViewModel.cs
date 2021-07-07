using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Core.Features.Products
{
    public class ProductPageViewModel
    {
        public ProductPageViewModel(IProduct product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.ImageUrl = product.ImageUrl;
            this.Price = product.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string UrlSegment => $"{Id}/{Name}";
    }
}
