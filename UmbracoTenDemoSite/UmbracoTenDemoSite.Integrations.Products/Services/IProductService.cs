using System.Collections.Generic;
using UmbracoTenDemoSite.Integrations.Products.Entities;

namespace UmbracoTenDemoSite.Integrations.Products.Services
{
    public interface IProductService
    {
        IProduct Get(int id);
        IEnumerable<IProduct> GetAll();
    }
}
