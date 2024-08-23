using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class OrderController : Controller
    {
        //private readonly ICartService _cartService;
        private readonly ICart _cart;
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<OrderController> _localizer;

        public OrderController(ICart pCart, IOrderService service, IStringLocalizer<OrderController> localizer)
        {
            _cart = pCart ?? throw new ArgumentNullException(nameof(pCart));
            //_cartService = cartService;
            _orderService = service;
            _localizer = localizer;
        }


        /// <summary>
        /// Affiche le formulaire de commande.
        /// </summary>
        public IActionResult Checkout()
        {
            try
            {
                var cart = Cart.GetCart(HttpContext.Session);

                if (!cart.Lines.Any())
                {
                    ModelState.AddModelError("", _localizer["CartEmpty"]);
                    return RedirectToAction("Index", "Cart");
                }

                return View(new Order());
            }
            catch (Exception ex)
            {
                // Log l'exception ici si nécessaire
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /* [HttpPost]
          public IActionResult Index(Order order)
          {
              if (!((Cart) _cart).Lines.Any())
              {
                  ModelState.AddModelError("", _localizer["CartEmpty"]);
              }
              if (ModelState.IsValid)
              {
                  order.Lines = (_cart as Cart)?.Lines.ToArray();
                  _orderService.SaveOrder(order);
                  return RedirectToAction(nameof(Completed));
              }
              else
              {
                  return View(order);
              }
          }*/

        /*public IActionResult Checkout()
        {
            try
            {
                var cart = Cart.GetCart(HttpContext.Session);

                if (!cart.Lines.Any())
                {
                    ModelState.AddModelError("", "Votre panier est vide!");
                    return RedirectToAction("Index", "Cart");
                }

                return View(new Order());
            }
            catch (Exception ex)
            {
                // Log l'exception ici si nécessaire
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/

        /// <summary>
        /// Traite la commande une fois le formulaire soumis.
        /// </summary>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cart = Cart.GetCart(HttpContext.Session);

            if (ModelState.IsValid)
            {
                // Ajouter les lignes de commande au modèle Order
                order.Lines = cart.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction("Completed");
                //return RedirectToAction(nameof(Completed));
            }

            return View(order);
        }

       /* [HttpPost]*/
       /* public IActionResult Checkout(Order order)
        {
            var cart = Cart.GetCart(HttpContext.Session);

            if (ModelState.IsValid)
            {
                // Ajouter les lignes de commande au modèle Order
                order.Lines = cart.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction("Completed");
            }
            return View(order);
        }*/

        public ViewResult Completed()
        {
            // Utilisation de la méthode GetCart pour récupérer le panier depuis la session
            var cart = Cart.GetCart(HttpContext.Session);

            if (cart != null)
            {
                cart.Clear();
            }

            return View();
        }

        /// <summary>
        /// Utilisé pour soumettre le formulaire à l'action Index (au cas où).
        /// </summary>
        /*[HttpPost]
        public IActionResult Index(Order order)
        {
            var cart = Cart.GetCart(HttpContext.Session);

            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", _localizer["CartEmpty"]);
                return View(order);
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }

            return View(order);
        }*/
    }
}
