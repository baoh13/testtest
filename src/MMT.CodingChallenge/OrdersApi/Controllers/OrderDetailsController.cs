using Application.OrderDetails.Queries.GetOrderDetails;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OrdersApi.Controllers
{
    public class OrderDetailsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> OrderDetails()
        {
            var query = new OrderDetailsQuery();
            var transactionDto = await Mediator.Send(query);

            return Ok("OrderDetails");
        }
    }
}
