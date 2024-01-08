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
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        private List<CartLine> _lines = new List<CartLine>();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            //Retourne la liste actuelle de cartline (auparavant retournait une nouvelle liste de CartLine à chaque appel...)
            return _lines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            var line = _lines.FirstOrDefault(l => l.Product.Id == product.Id);

            if (line == null)
            {
                _lines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double total = 0.0;
            for (int i = 0; i < _lines.Count; i++)
            {
                total += _lines[i].Product.Price * _lines[i].Quantity;
            }
            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            int nombreProduits = 0;
            double total = GetTotalValue();
            //Calcul du nombre de produits
            for (int i = 0; i < _lines.Count; i++)
            {
                nombreProduits += _lines[i].Quantity;
            }

            //On évite la division par 0
            if (nombreProduits != 0)
            {
                //Calcul de la moyenne
                double moyenne = total / nombreProduits;
                return moyenne;
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            foreach (var line in _lines)
            {
                if (line.Product.Id == productId)
                {
                    CartLine FDICLline = line;
                    return FDICLline.Product;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
