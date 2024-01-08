using System.Collections.Generic;
using System.Security.Cryptography;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models;
using System;


namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int id)
        {
            List <Product> products = _productRepository.GetAllProducts();
            Product product = products.Find(productGet => productGet.Id == id);
            return product;
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            try
            {

                if (cart != null)
            { 
                foreach (var cartLine in cart.Lines)
                {
                    int productId = cartLine.Product.Id;
                    int quantityToRemove = cartLine.Quantity;
                    Product product = GetProductById(productId);
                    if (product != null && product.Stock >= quantityToRemove)
                    {
                        _productRepository.UpdateProductStocks(productId, quantityToRemove);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nCart is null\n");  
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
    }
}
