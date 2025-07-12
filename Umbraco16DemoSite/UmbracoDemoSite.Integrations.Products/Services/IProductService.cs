using System.Collections.Generic;
using UmbracoDemoSite.Integrations.Products.Entities;

namespace UmbracoDemoSite.Integrations.Products.Services
{
    public interface IProductService
    {
        IProduct? Get(int id);
        IEnumerable<IProduct>? GetAll();
    }
}
