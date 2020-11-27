using Application.AppSetting;
using Application.Dtos;
using Application.interfaces;
using Application.OrderDetails.Queries.GetOrderDetails;
using Infrastructure.ApiClient;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CustomerDetailsService : ICustomerDetailsService
    {
        private readonly AppSettings _appSettings;

        public CustomerDetailsService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<CustomerDetailsDto> GetCustomerDetails(OrderDetailsQuery request)
        {
            var baseUrl = _appSettings.CustomerDetailsServiceBaseUrl;
            var code = _appSettings.CustomerDetailsServiceCode;
            var email = request.User;

            var endpoint = $"/api/GetUserDetails?code={code}&&email={email}";

            var client = new CustomerDetailsClient(baseUrl);

            var customerDetailsDto = await client.Process(endpoint);

            return customerDetailsDto;
        }
    }
}
