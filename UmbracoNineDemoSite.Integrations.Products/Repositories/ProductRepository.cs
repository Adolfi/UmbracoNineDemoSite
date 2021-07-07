using System.Collections.Generic;
using System.Linq;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Integrations.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// These are just fake products. Here is where you would add your own logic for fetching product from an external source, like an API for example.
        /// </summary>
        public IEnumerable<Product> FakeProducts => new List<Product>()
        {
            new Product(1, "Biker Jacket", 199, "/media/sr3g0xvt/bikerjacket.jpg"),
            new Product(2, "Tattoo", 499, "/media/g01hjwk2/tattoo.jpg"),
            new Product(3, "Unicorn", 249, "/media/jc2d4ta1/unicorn.jpg"),
            new Product(4, "Ping Pong Ball", 2, "/media/tkdi2gz3/pingpongball.jpg"),
            new Product(5, "Bowling Ball", 899, "/media/aftcj4cv/bowlingball.jpg"),
            new Product(6, "Jumpsuit", 89, "/media/2y2bmpff/jumpsuit.jpg"),
            new Product(7, "Banjo", 399, "/media/fb3nis3w/banjo.jpg"),
            new Product(8, "Knitted Unicorn West", 1899, "/media/prkdzma4/west.jpg")
        };

        public IProduct Get(int id)
        {
            return this.FakeProducts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<IProduct> GetAll()
        {
            return this.FakeProducts;
        }
    }
}
