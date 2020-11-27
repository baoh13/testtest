using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.OrderDetails.Queries.GetOrderDetails
{
    public class OrderDetailsQueryHandler : IRequestHandler<OrderDetailsQuery, OrderDetailsDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICustomerDetailsService _customerDetailsService;

        public OrderDetailsQueryHandler(IAppDbContext dbContext, ICustomerDetailsService customerDetailsService)
        {
            _dbContext = dbContext;
            _customerDetailsService = customerDetailsService;
        }

        public async Task<OrderDetailsDto> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetailsDto = new OrderDetailsDto();
            var customerDto = await _customerDetailsService.GetCustomerDetails(request);

            var mostRecentOrder = _dbContext.Orders
                                   .Where(o => o.CustomerId == customerDto.CustomerId)
                                   .OrderByDescending(o => o.OrderDate)
                                   .FirstOrDefault();

            var orderItems = mostRecentOrder != null 
                            ? _dbContext.OrderItems.Include(o => o.Product).Where(oi => oi.OrderId == mostRecentOrder.OrderId).ToList()
                            : new List<OrderItem>();

            orderDetailsDto = MapToOrderDetailsDto(customerDto, mostRecentOrder, orderItems);

            return orderDetailsDto;
        }

        private OrderDetailsDto MapToOrderDetailsDto(
            Dtos.CustomerDetailsDto customerDto, 
            Order order, 
            List<OrderItem> orderItems)
        {
            var orderDetailsDto = new OrderDetailsDto();
            var orderDto = new OrderDto();
            orderDetailsDto.Customer = new CustomerDto()
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName
            };

            if (order != null)
            {
                orderDto = new OrderDto()
                {
                    DeliveryAddress = GetDeliveryAddress(customerDto),
                    OrderNumber = order.OrderId,
                    OrderItems = GetOrderItems(orderItems, order.ContainsGift),
                    DeliveryExpected = order.DeliveryExpected.ToString("dd-MMMM-yyyy"),
                    OrderDate = order.OrderDate.ToString("dd-MMMM-yyyy"),
                };
            }

            orderDetailsDto.Order = orderDto;

            return orderDetailsDto;
        }

        private List<OrderItemDto> GetOrderItems(List<OrderItem> orderItems, bool containsGift)
        {
            var orderItemsDto = new List<OrderItemDto>();

            orderItems.ForEach(o =>
            {
                var orderItem = new OrderItemDto()
                {
                    PriceEach = o.Price ?? 0,
                    Product = containsGift ? "Gift" : o.Product.ProductName,
                    Quantity = o.Quantity
                };

                orderItemsDto.Add(orderItem);
            });

            return orderItemsDto;
        }

        private string GetDeliveryAddress(Dtos.CustomerDetailsDto customerDto)
        {
            //"deliveryAddress": "1A Uppingham Gate, Uppingham, LE15 9NY",
            return $"{customerDto.HouseNumber} {customerDto.Street}, {customerDto.Town}, {customerDto.Postcode}";
        }
    }
}
