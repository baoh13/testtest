using Application.Dtos;
using Application.Exceptions;
using RestSharp;
using System.Threading.Tasks;

namespace Infrastructure.ApiClient
{
    public class CustomerDetailsClient : ClientBase
    {
        public CustomerDetailsClient(string baseUrl) : base(baseUrl)
        {}

        public async Task<CustomerDetailsDto> Process(string endpoint)
        {
            RestRequest restRequest = new RestRequest(endpoint, Method.GET);
            IRestResponse<CustomerDetailsDto> response = await ExecuteTaskAsync<CustomerDetailsDto>(restRequest);
            var dto = response.Data;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new CustomerDetailsHttpResponseException();
                //Todo Log HttpResponseErrors
            }

            return dto;
        }
    }
}
