using Microsoft.Extensions.Caching.Memory;
using P2FixAnAppDotNetCode.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMemoryCache _memoryCache;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            if (!_memoryCache.TryGetValue("AllProducts", out List<Product> products))
            {
                var productArray = _productRepository.GetAllProducts();
                products = productArray.ToList();
                _memoryCache.Set("AllProducts", products);
            }

            return products;
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            return GetAllProducts().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(int productId, int quantityToRemove)
        {
            _productRepository.UpdateProductStocks(productId, quantityToRemove);
            _memoryCache.Remove("AllProducts");

            var updatedProductList = _productRepository.GetAllProducts().ToList();
            _memoryCache.Set("AllProducts", updatedProductList);
        }

    }
}
