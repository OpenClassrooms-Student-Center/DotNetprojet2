using System;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services to manage an order
    /// </summary>
    public class OrderService : IOrderService
    {
       private readonly ICart _cart;
       private readonly IOrderRepository _repository;
       private readonly IProductService _productService;
        public OrderService(ICart cart, IOrderRepository orderRepo, IProductService productService)
        {
            _repository = orderRepo;
            _cart = cart;
            _productService = productService;
        }

        /// <summary>
        /// Saves an order
        /// </summary>
        public void SaveOrder(Order order)
        {
            order.Date = DateTime.Now;
            _repository.Save(order);
            
            foreach (var cartLine in (_cart as Cart).Lines)
            {
                _productService.UpdateProductQuantities(cartLine.Product.Id, cartLine.Quantity);
            }
            _cart.Clear();
        }
    }
}
