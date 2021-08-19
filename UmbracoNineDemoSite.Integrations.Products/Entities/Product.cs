namespace UmbracoNineDemoSite.Integrations.Products.Entities
{
    public class Product : IProduct
    {
        public Product(int id, string name, int price, string imageUrl, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Price { get; set; }        
    }
}
