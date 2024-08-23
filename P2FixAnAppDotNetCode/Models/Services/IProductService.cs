using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IProductService
    {
        //Product[] GetAllProducts();
        List<Product> GetAllProducts();
        void UpdateProductQuantities(Cart cart);
        Product GetProductById(int id);
    }
}
