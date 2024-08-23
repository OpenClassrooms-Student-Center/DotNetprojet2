using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using P2FixAnAppDotNetCode.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> _cartLines;

        private ISession _session;
        private readonly IProductService _productService;

        public Cart(ISession session, IProductService productService)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _cartLines = new List<CartLine>();
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            LoadCart(); // Charger le panier depuis la session si déjà existant
        }
        public Cart() { }
        private void LoadCart()
        {
            var cartJson = _session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                _cartLines = JsonConvert.DeserializeObject<List<CartLine>>(cartJson);
            }
        }

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        //public IEnumerable<CartLine> Lines => GetCartLineList();
        public IEnumerable<CartLine> Lines => _cartLines;


        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            var cartLine = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);

            if (cartLine == null)
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
            SaveCart();
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product)
        {
            _cartLines.RemoveAll(l => l.Product.Id == product.Id);
            SaveCart();
        }

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            return _cartLines.Sum(l => l.Product.Price * l.Quantity);
        }

        private void SaveCart()
        {
            var cartJson = JsonConvert.SerializeObject(_cartLines);
            _session.SetString("Cart", cartJson);
        }

        public static Cart GetCart(ISession session, IProductService productService)
        {
            return new Cart(session, productService);
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            return _cartLines.Count > 0 ? _cartLines.Average(l => l.Product.Price) : 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return _cartLines.FirstOrDefault(l => l.Product.Id == productId)?.Product;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return _cartLines.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
     
        public void Clear()
        {
            /*foreach (var line in _cartLines)
            {
                _productService.UpdateProductQuantities(line.Product, -line.Quantity);
            }*/
            _cartLines.Clear();

            SaveCart();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
