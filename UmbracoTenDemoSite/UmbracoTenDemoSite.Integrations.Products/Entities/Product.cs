namespace UmbracoTenDemoSite.Integrations.Products.Entities
{
	public class Product : IProduct
	{
		private const int maxShortDescriptionLength = 50;
		public Product(int id, string name, int price, string imageUrl, string description, string shortDescription = null)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.Price = price;
			this.ImageUrl = imageUrl;
			this.ShortDescription = shortDescription ??
				(description.Length > maxShortDescriptionLength
					? description.Substring(0, maxShortDescriptionLength - 3) + "..."
					: description);
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public int Price { get; set; }
		public string ShortDescription { get; set; }
	}
}
