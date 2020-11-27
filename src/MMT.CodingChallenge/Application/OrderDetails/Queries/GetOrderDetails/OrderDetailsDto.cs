using System.Collections.Generic;

namespace Application.OrderDetails.Queries.GetOrderDetails

{
    public class OrderDetailsDto
    {
        public CustomerDto Customer { get; set; }
        public OrderDto Order { get; set; }
    }

    public class CustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OrderDto
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryExpected { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceEach { get; set; }
    }
}
