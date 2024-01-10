using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Controllers;
using P2FixAnAppDotNetCode.Models.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Xunit;
using Microsoft.Extensions.Localization;
using StackExchange.Redis;
using Order = P2FixAnAppDotNetCode.Models.Order;
using Moq;
using System.Linq;

namespace P2FixAnAppDotNetCode.Tests
{
    public class OrderControllerTests
    {
        private Mock<ICart> _mockCart;
        private Mock<IOrderService> _mockOrderService;
        private Mock<IStringLocalizer<OrderController>> _mockLocalizer;
        private Mock<Order> _mockOrder;
        private OrderController _controller;

        public OrderControllerTests()
        {
            _mockCart = new Mock<ICart>();
            _mockOrderService = new Mock<IOrderService>();
            _mockLocalizer = new Mock<IStringLocalizer<OrderController>>();
            _controller = new OrderController(_mockCart.Object, _mockOrderService.Object, _mockLocalizer.Object);
        }

        [Fact]
        public void Index_WhenCartIsEmpty_AddsModelError()
        {
            // Arrange
            Cart cart = new Cart();
            _mockLocalizer.Setup(l => l["CartEmpty"]).Returns(new LocalizedString("CartEmpty", "The cart is empty."));
            _controller = new OrderController(cart, _mockOrderService.Object, _mockLocalizer.Object);

            var order = new Order();

            // Act
            var result = _controller.Index(order) as ViewResult;

            // Assert
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal("The cart is empty.", _controller.ModelState[""].Errors.First().ErrorMessage);
            Assert.NotNull(result);
            Assert.Equal(order, result?.Model);
        }

        [Fact]
        //Le Panier n'est pas vide et le formulaire est rempli correctement
        public void Index_CartNotEmptyModelStateValid_ModelStateValid()
        {
            // Arrange
            Cart cart = new Cart();
            Product product1 = new Product(1, 1, 20, "name", "description");
            cart.AddItem(product1, 1);
            _controller = new OrderController(cart, _mockOrderService.Object, _mockLocalizer.Object);

            var order = new Order()
            {
                Name = "NomDeTest",
                Address = "AdresseTest",
                City = "VilleTest",
                Zip = "ZipTest",
                Country = "PaysTest",
            };

            // Act
            var result = _controller.Index(order) as ViewResult;
            // Assert
            Assert.True(_controller.ModelState.IsValid, "Not Empty Cart and Model is Valid");
        }
    }

}