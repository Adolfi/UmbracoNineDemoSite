using System.Collections.Generic;
using UmbracoNineDemoSite.Integrations.Products.Entities;
using UmbracoNineDemoSite.Integrations.Products.Repositories;

namespace UmbracoNineDemoSite.Integrations.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IProduct Get(int id)
        {
            // TODO: Add Caching
            // TODO: Use Configurations
            return this.productRepository.Get(id);
        }

        public IEnumerable<IProduct> GetAll()
        {
            // TODO: Add Caching
            // TODO: Use Configurations
            return this.productRepository.GetAll();
        }
    }
}
