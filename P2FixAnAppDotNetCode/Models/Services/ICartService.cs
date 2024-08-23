namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface ICartService
    {
        void AddItem(Product product, int quantity);
        void RemoveLine(Product product);
        void Clear();
        double GetTotalValue();
        double GetAverageValue();
        Cart GetCart();
    }
}
