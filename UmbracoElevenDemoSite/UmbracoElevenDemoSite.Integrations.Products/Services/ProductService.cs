using System.Collections.Generic;
using System.Linq;
using UmbracoElevenDemoSite.Integrations.Products.Entities;

namespace UmbracoElevenDemoSite.Integrations.Products.Services
{
    public class ProductService : IProductService
    {
        public IProduct Get(int id)
        {
            return this.products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<IProduct> GetAll()
        {
            return this.products;
        }

        /// <summary>
        /// These are just fake products. Here is where you would add your own logic for fetching product from an external source, like an API or a separate database/table for example.
        /// </summary>
        private IEnumerable<Product> products => new List<Product>()
        {
            new Product(1, "Biker Jacket", 199, "/media/sr3g0xvt/bikerjacket.jpg", "Donec rutrum congue leo eget malesuada. Vivamus suscipit tortor eget felis porttitor volutpat.", "Explicit SEO meta description."),
            new Product(2, "Tattoo", 499, "/media/g01hjwk2/tattoo.jpg", "Cras ultricies ligula sed magna dictum porta."),
            new Product(3, "Unicorn", 249, "/media/jc2d4ta1/unicorn.jpg", "Quisque velit nisi, pretium ut lacinia in, elementum id enim. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Cras ultricies ligula sed magna dictum porta."),
            new Product(4, "Ping Pong Ball", 2, "/media/tkdi2gz3/pingpongball.jpg", "Vivamus suscipit tortor eget felis porttitor volutpat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras ultricies ligula sed magna dictum porta."),
            new Product(5, "Bowling Ball", 899, "/media/aftcj4cv/bowlingball.jpg", "Cras ultricies ligula sed magna dictum porta."),
            new Product(6, "Jumpsuit", 89, "/media/2y2bmpff/jumpsuit.jpg", "Proin eget tortor risus. Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Quisque velit nisi, pretium ut lacinia in, elementum id enim. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Cras ultricies ligula sed magna dictum porta."),
            new Product(7, "Banjo", 399, "/media/fb3nis3w/banjo.jpg", "Vivamus suscipit tortor eget felis porttitor volutpat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam vel, ullamcorper sit amet ligula. Proin eget tortor risus."),
            new Product(8, "Knitted Unicorn West", 1899, "/media/prkdzma4/west.jpg", "Knitted Unicorn West.")
        };
    }
}
