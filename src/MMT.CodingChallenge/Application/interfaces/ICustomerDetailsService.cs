using Application.Dtos;
using Application.OrderDetails.Queries.GetOrderDetails;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface ICustomerDetailsService
    {
        Task<CustomerDetailsDto> GetCustomerDetails(OrderDetailsQuery request);
    }
}
