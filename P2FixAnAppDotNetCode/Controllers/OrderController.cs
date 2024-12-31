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
        private readonly ICart _cart;
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<OrderController> _localizer;
        private readonly IProductService _productService;

        public OrderController(ICart pCart, IProductService productService, IOrderService service, IStringLocalizer<OrderController> localizer)
        {
            _cart = pCart ?? throw new ArgumentNullException(nameof(pCart));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
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
                var cart = Cart.GetCart(HttpContext.Session, _productService);

                if (!cart.Lines.Any())
                {
                    ModelState.AddModelError("", _localizer["CartEmpty"]);
                    return RedirectToAction("Index", "Cart");
                }

                return View(new Order());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Traite la commande une fois le formulaire soumis.
        /// </summary>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction("Completed");
            }

            return View(order);
        }

        public ViewResult Completed()
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);

            if (cart != null)
            {
                cart.Clear();
            }

            return View();
        }
    }
}
