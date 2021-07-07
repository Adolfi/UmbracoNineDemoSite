using System.Collections.Generic;
using UmbracoNineDemoSite.Integrations.Products.Entities;

namespace UmbracoNineDemoSite.Integrations.Products.Repositories
{
    public interface IProductRepository
    {
        IProduct Get(int id);
        IEnumerable<IProduct> GetAll();
    }
}
