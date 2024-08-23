using System.Linq;
using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IProductService _productService;

        public CartController(ICart cart ,IProductService productService)
        {
            _cart = cart;
            _productService = productService;
        }

        public ViewResult Index()
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);
            return View(cart);
        }
        
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                cart.AddItem(product, quantity);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var cart = Cart.GetCart(HttpContext.Session, _productService);
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}
