using System.Collections.Generic;
using UmbracoElevenDemoSite.Integrations.Products.Entities;

namespace UmbracoElevenDemoSite.Integrations.Products.Services
{
    public interface IProductService
    {
        IProduct Get(int id);
        IEnumerable<IProduct> GetAll();
    }
}
