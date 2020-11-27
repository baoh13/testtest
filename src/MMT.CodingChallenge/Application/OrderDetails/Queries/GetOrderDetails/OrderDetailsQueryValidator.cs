using Application.Exceptions;
using FluentValidation;
using System.Collections.Generic;

namespace Application.OrderDetails.Queries.GetOrderDetails
{
    public class OrderDetailsQueryValidator: AbstractValidator<OrderDetailsQuery>
    {
        private IDictionary<string, string> _customers = new Dictionary<string, string>();

        public OrderDetailsQueryValidator()
        {
            InitializeCustomers();

            RuleFor(c => c.User)
                .Must((query, user) => MatchId(query))
                .WithMessage("User does not exist.");

            RuleFor(c => c.CustomerId)
                .NotEmpty().WithMessage("CustomerId is Required");
        }

        private bool MatchId(OrderDetailsQuery query)
        {
            if (_customers.ContainsKey(query.User))
            {
                if (_customers[query.User] != query.CustomerId)
                {
                    throw new InvalidUser();
                }
                return true;
            }

            throw new InvalidUser();
        }

        private void InitializeCustomers()
        {
            _customers.Add("cat.owner@mmtdigital.co.uk", "C34454");
            _customers.Add("dog.owner@fake-customer.com", "R34788");
            _customers.Add("sneeze@fake-customer.com", "A99001");
            _customers.Add("santa@north-pole.lp.com", "XM45001");
        }
    }
}
