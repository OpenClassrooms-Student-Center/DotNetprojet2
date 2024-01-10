using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models.Services;
using Xunit;

namespace P2FixAnAppDotNetCode.Tests
{
    /// <summary>
    /// The Cart test class
    /// </summary>
    public class CartTests
    {
        [Fact]
        public void AddItemInCart_Add2ItemID1_LinesCount1Quantity2()
        {
            //Arrange
            Cart cart = new Cart();
            Product product1 = new Product(1, 10, 20, "name", "description");

            //Act
            cart.AddItem(product1, 1);
            cart.AddItem(product1, 1);

            //Assert
            Assert.NotEmpty(cart.Lines); //Vérifie que la liste des lignes n'est pas vide
            Assert.Single(cart.Lines); //Vérifie qu'il n'y a qu'une seule ligne

        }

        [Fact]
        public void AddItemInCart_Add2DifferentsItems_LinesCount2Quantity1()
        {
            //Arrange
            Cart cart = new Cart();
            Product product1 = new Product(1, 10, 20, "name", "description");
            Product product2 = new Product(2, 10, 20, "name", "description");

            //Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2); //On ajoute 2 fois le product2

            //Assert
            Assert.Equal(1, cart.Lines.First().Quantity); //Vérifie la quantité de la ligne0 est bien 1
            Assert.Equal(2, cart.GetCartLineByIndex(1).Quantity); //Vérifie la quantité de la ligne1 est bien 2
            Assert.Equal(2, cart.Lines.Count()); //Vérifie qu'il y a bien deux lignes dans le panier
        }

        [Fact]
        public void AddItemInCart_NoStock_NotAdd()
        {
            //Arrange
            Cart cart = new Cart();
            Product product1 = new Product(1, 1, 20, "name", "description");
            Product product2 = new Product(2, 0, 20, "name", "description"); //produit n'est plus en stock

            //Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 0); //On ajoute 1 fois le product2 qui n'est plus en stock

            //Assert
            Assert.Equal(1, cart.Lines.First().Quantity); //Vérifie la quantité de la ligne0 est bien 1
            Assert.Single(cart.Lines); //Vérifie qu'aucune ligne n'a été ajouté puisque le produit n'est plus en stock (donc il y a juste l'article 1
            Assert.Single(cart.Lines); //Vérifie qu'il y a bien une seule ligne dans le panier => le produit n'a pas pu être ajoute
        
        }

        [Fact]
        public void AddItemInCart_NoEnoughStock_MaximumAdd()
        {
            //Arrange
            Cart cart = new Cart();
            Product product1 = new Product(1, 1, 20, "name", "description");
            Product product2 = new Product(2, 10, 20, "name", "description"); 

            //Act
            cart.AddItem(product1, 1);
            cart.AddItem(product2, 99); //On ajoute 99 fois le product2 qui n'a plus que 10 product en stock

            //Assert
            Assert.Equal(1, cart.Lines.First().Quantity); //Vérifie la quantité de la ligne0 est bien 1
            Assert.Equal(10, cart.GetCartLineByIndex(1).Quantity); //Vérifie la quantité ajouté du produit 2 est bien 10 (le maximum disponible)

            //Act
            cart.AddItem(product2, 1); //On ajoute 1 fois le product2 qui n'a plus de product en stock suite à l'opération précédente

            //Assert
            Assert.Equal(10, cart.GetCartLineByIndex(1).Quantity); //Vérifie la quantité ajouté du produit 2 est bien 10 - rien n'a été ajouté


        }

        [Fact]
        public void GetAverageValue()
        {
            ICart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 2), 2);
            cart.AddItem(products.First(p => p.Id == 5), 1);
            double averageValue = cart.GetAverageValue();
            double expectedValue = (9.99 * 2 + 895.00) / 3;

            Assert.Equal(expectedValue, averageValue);
        }

        [Fact]
        public void GetTotalValue()
        {
            ICart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IOrderRepository orderRepository = new OrderRepository();
            IProductService productService = new ProductService(productRepository, orderRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.First(p => p.Id == 1), 1);
            cart.AddItem(products.First(p => p.Id == 4), 3);
            cart.AddItem(products.First(p => p.Id == 5), 1);
            double totalValue = cart.GetTotalValue();
            double expectedValue = 92.50 + 32.50 * 3 + 895.00;

            Assert.Equal(expectedValue, totalValue);
        }

        [Fact]
        public void FindProductInCartLines()
        {
            Cart cart = new Cart();
            Product product = new Product(999, 1, 20, "name", "description");

            cart.AddItem(product, 1);
            Product result = cart.FindProductInCartLines(999);

            Assert.NotNull(result); //Vérifie que le produit existe
        }
    }
}
