using RestSharp;
using System;
using System.Threading.Tasks;

namespace Infrastructure.ApiClient
{
    public class ClientBase: RestClient
    {
        public ClientBase(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        public override async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            var response = await base.ExecuteTaskAsync<T>(request);
            return response;
        }
    }
}
