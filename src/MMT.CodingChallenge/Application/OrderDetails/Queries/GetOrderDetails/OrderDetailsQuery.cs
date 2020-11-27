﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderDetails.Queries.GetOrderDetails
{
    public class OrderDetailsQuery: IRequest<OrderDetailsDto>
    {
        public string User { get; set; }
        public string CustomerId { get; set; }
    }
}
