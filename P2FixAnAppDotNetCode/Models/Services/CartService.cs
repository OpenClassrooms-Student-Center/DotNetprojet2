namespace P2FixAnAppDotNetCode.Models.Services
{
    public class CartService : ICartService
    {
        private readonly ICart _cart;

        public CartService(ICart cart)
        {
            _cart = cart;
        }

        public void AddItem(Product product, int quantity)
        {
            _cart.AddItem(product, quantity);
        }

        public void RemoveLine(Product product)
        {
            _cart.RemoveLine(product);
        }

        public void Clear()
        {
            _cart.Clear();
        }

        public double GetTotalValue()
        {
            return _cart.GetTotalValue();
        }

        public double GetAverageValue()
        {
            return _cart.GetAverageValue();
        }

        public Cart GetCart()
        {
            return (Cart)_cart;
        }
    }
}
