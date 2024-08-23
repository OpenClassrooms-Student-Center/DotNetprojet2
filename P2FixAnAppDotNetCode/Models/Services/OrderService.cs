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
       /* public void SaveOrder(Order order)
        {
            order.Date = DateTime.Now;
            _repository.Save(order);
            _productService.UpdateProductQuantities(_cart as Cart);
            _cart.Clear();
        }*/

        public void SaveOrder(Order order)
        {
            // Définir la date de la commande
            order.Date = DateTime.Now;

            // Sauvegarder la commande dans le repository
            _repository.Save(order);

            // Mettre à jour les stocks pour chaque produit dans le panier
            foreach (var cartLine in (_cart as Cart).Lines)
            {
                _productService.UpdateProductQuantities(cartLine.Product.Id, cartLine.Quantity);
            }

            // Vider le panier après avoir sauvegardé la commande et mis à jour les stocks
            _cart.Clear();
        }


        /// <summary>
        /// Update the product quantities inventory and clears the cart
        /// </summary>
       /* private void UpdateInventory()
        {
            _productService.UpdateProductQuantities(_cart as Cart);
            _cart.Clear();
        }*/
    }
}
