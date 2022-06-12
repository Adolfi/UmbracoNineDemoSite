using System.Collections.Generic;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Integrations.Products.Services
{
    public interface IProductService
    {
        IProduct Get(int id);
        IEnumerable<IProduct> GetAll();
    }
}
