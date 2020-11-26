using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.interfaces;

namespace Application.OrderDetails.Queries.GetOrderDetails
{
    public class OrderDetailsQueryHandler : IRequestHandler<OrderDetailsQuery, OrderDetailsDto>
    {
        private readonly IAppDbContext _dbContext;

        public OrderDetailsQueryHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<OrderDetailsDto> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var dto = new OrderDetailsDto();            

            return Task.FromResult(dto);
        }
    }
}
