using Application.Dtos;
using Application.interfaces;
using Application.OrderDetails.Queries.GetOrderDetails;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace Application.UnitTests.OrderDetails.Queries
{
    using static TestSetup;
    public class OrderDetailsQueryTests
    {
        private Mock<IAppDbContext> _context;
        private Mock<ICustomerDetailsService> _customerDetailsService;
        private OrderDetailsQueryHandler _SUT;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<IAppDbContext>();
            _customerDetailsService = new Mock<ICustomerDetailsService>();

        }

        [Test]
        public async Task It_Fetches_Most_Recent_Order_With_Gifts_As_OrderItems()
        {
            var request = new OrderDetailsQuery()
            {
                CustomerId = "1",
                User = "User"
            };

            var customerDetailsDto = new CustomerDetailsDto() 
            { 
                CustomerId = "1",
                FirstName = "FirstName",
                LastName = "LastName",
                HouseNumber = "1A",
                Postcode = "PostCode",
                Street = "Street",
                Town = "Town"                
            };            

            _customerDetailsService.Setup(s => s.GetCustomerDetails(It.IsAny<OrderDetailsQuery>()))
                                   .ReturnsAsync(customerDetailsDto);

            var order3 = CreateOrder(containsGift: true, customerId: "1", orderDate: new System.DateTime(2020, 10, 20), orderId: 3);
            var order4 = CreateOrder(containsGift: false, customerId: "1", orderDate: new System.DateTime(2020, 10, 19), orderId: 4);

            var orders = new List<Order> { order3, order4 };

            SeedContext(orders: orders);

            _SUT = new OrderDetailsQueryHandler(_context.Object, _customerDetailsService.Object);

            var orderDetailsDto = await _SUT.Handle(request, new System.Threading.CancellationToken());

            orderDetailsDto.Order.OrderNumber.Should().Be(3);
            orderDetailsDto.Order.DeliveryAddress.Should().Be("1A Street, Town, PostCode");
            orderDetailsDto.Order.OrderDate.Should().Be("20-October-2020");
            orderDetailsDto.Order.OrderItems.Count.Should().Be(2);
            orderDetailsDto.Order.OrderItems[0].Product.Should().Be("Gift");
            orderDetailsDto.Order.OrderItems[1].Product.Should().Be("Gift");
        }

        [Test]
        public async Task It_Fetches_Most_Recent_Order_With_Normal_OrderItems()
        {
            var request = new OrderDetailsQuery()
            {
                CustomerId = "1",
                User = "User"
            };

            var customerDetailsDto = new CustomerDetailsDto()
            {
                CustomerId = "1",
                FirstName = "FirstName",
                LastName = "LastName",
                HouseNumber = "2A",
                Postcode = "PostCode",
                Street = "Street",
                Town = "Town"
            };

            _customerDetailsService.Setup(s => s.GetCustomerDetails(It.IsAny<OrderDetailsQuery>()))
                                   .ReturnsAsync(customerDetailsDto);

            var order3 = CreateOrder(containsGift: true, customerId: "1", orderDate: new System.DateTime(2020, 10, 20), orderId: 3);
            var order4 = CreateOrder(containsGift: false, customerId: "1", orderDate: new System.DateTime(2020, 10, 21), orderId: 4);

            var orders = new List<Order> { order3, order4 };

            SeedContext(orders: orders);

            _SUT = new OrderDetailsQueryHandler(_context.Object, _customerDetailsService.Object);

            var orderDetailsDto = await _SUT.Handle(request, new System.Threading.CancellationToken());

            orderDetailsDto.Order.OrderNumber.Should().Be(4);
            orderDetailsDto.Order.DeliveryAddress.Should().Be("2A Street, Town, PostCode");
            orderDetailsDto.Order.OrderDate.Should().Be("21-October-2020");
            orderDetailsDto.Order.OrderItems.Count.Should().Be(1);
            orderDetailsDto.Order.OrderItems[0].Product.Should().Be("Toy1");
        }

        private void SeedContext(List<Order> orders = null, List<OrderItem> orderItems = null, List<Product> products = null)
        {           
            var orderItem1 = CreateOrderItem(orderId: 3, price: 9, productId: 12, productName: "Toy1");
            var orderItem2 = CreateOrderItem(orderId: 3, price: 8, productId: 13, productName: "Toy2");
            var orderItem3 = CreateOrderItem(orderId: 4, price: 8, productId: 12, productName: "Toy1");
            orderItems = orderItems ?? new List<OrderItem>() { orderItem1, orderItem2, orderItem3 };

            var product1 = CreateProduct(productId: 12, productName: "Toy1");
            var product2 = CreateProduct(productId: 13, productName: "Toy2");
            products = products ?? new List<Product>() { product1, product2 };


            DbSet<Order> orderDbSet = GetQueryableMockDbSet(orders);
            DbSet<OrderItem> orderItemDbSet = GetQueryableMockDbSet(orderItems);
            DbSet<Product> productsDbSet = GetQueryableMockDbSet(products);

            _context.Setup(c => c.Orders).Returns(orderDbSet);
            _context.Setup(c => c.OrderItems).Returns(orderItemDbSet);
            _context.Setup(c => c.Products).Returns(productsDbSet);
        }
    }
}
