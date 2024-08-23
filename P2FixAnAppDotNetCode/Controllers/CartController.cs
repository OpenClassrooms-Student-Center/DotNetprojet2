using System.Linq;
using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        //private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICart cart ,IProductService productService)
        {
            _cart = cart;
            //_cartService = cartService;
            _productService = productService;
        }

        public ViewResult Index()
        {
            var cart = Cart.GetCart(HttpContext.Session);
            return View(cart);
        }

        /* public ViewResult Index()
         {
             var cart = _cart.GetCart();
             return View(cart);
         }*/

        /* [HttpPost]
         public RedirectToActionResult AddToCart(int id)
         {
             Product product = _productService.GetProductById(id);

             if (product != null)
             {
                 _cart.AddItem(product, 1);
                 return RedirectToAction("Index");
             }
             else
             {
                 return RedirectToAction("Index", "Product");
             }
         }*/

        /*public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _cart.AddItem(product, quantity);
            }
            return RedirectToAction("Index");
        }*/
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart.GetCart(HttpContext.Session);
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                cart.AddItem(product, quantity);
            }

            return RedirectToAction("Index");
        }

        /*public RedirectToActionResult RemoveFromCart(int id)
        {
            Product product = _productService.GetAllProducts()
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }*/

        /* public IActionResult RemoveFromCart(int id)
         {
             var product = _productService.GetProductById(id);
             if (product != null)
             {
                 _cart.RemoveLine(product);
             }
             return RedirectToAction("Index");
         }*/

        public IActionResult RemoveFromCart(int id)
        {
            var cart = Cart.GetCart(HttpContext.Session);
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index");
        }

        /* public IActionResult ClearCart()
         {
             _cart.Clear();
             return RedirectToAction("Index");
         }*/

        public IActionResult ClearCart()
        {
            var cart = Cart.GetCart(HttpContext.Session);
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}
