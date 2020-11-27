using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.UnitTests
{
    public class TestSetup
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        public static Order CreateOrder(
            bool containsGift = false,
            string customerId = "Cust1",
            DateTime? orderDate = null,
            int orderId = 3)
        {
            var order = new Order()
            {
                ContainsGift = containsGift,
                CustomerId = customerId,
                DeliveryExpected = new DateTime(2020, 09, 10),
                OrderDate = orderDate ?? new DateTime(2020, 09, 09),
                OrderId = orderId,
                OrderSource = "WEB",
                ShippingMode = "WEB",
            };

            return order;
        }

        public static OrderItem CreateOrderItem(
            int orderId = 3, 
            decimal price = 9,
            int productId = 12,
            string productName = "Toy")
        {
            var orderItem = new OrderItem()
            {
                OrderId = orderId,
                OrderItemId = 1,
                Price = price,
                ProductId = productId,
                Product = CreateProduct(productName: productName, productId: productId),
                Order = CreateOrder()
            };

            return orderItem;
        }

        public static Product CreateProduct(string productName = "Toy", int productId = 12)
        {
            var product = new Product()
            {
                Colour = "Red",
                ProductId = 12,
                ProductName = productName,
                Size = "M"               
            };

            return product;
        }
    }
}
