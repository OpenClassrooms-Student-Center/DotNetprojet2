using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        void UpdateProductQuantities(int productId, int quantityToRemove);
        Product GetProductById(int id);
    }
}
