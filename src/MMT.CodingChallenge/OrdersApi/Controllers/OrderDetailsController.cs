using Application.Exceptions;
using Application.OrderDetails.Queries.GetOrderDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrdersApi.Controllers
{
    public class OrderDetailsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult> OrderDetails([FromBody]OrderDetailsQuery request)
        {
            try
            {
                var dto = await Mediator.Send(request);

                return Ok(dto);
            }
            catch(CustomerDetailsHttpResponseException)
            {
                return ActionResultHelper.ToBadRequestActionResult(message: "Error While Fetching Customer Details", title: "Error", statusCode: StatusCodes.Status500InternalServerError);
            }
            catch(InvalidUser ex)
            {
                return ActionResultHelper.ToBadRequestActionResult(message: ex.Message, title: "User Not Found", statusCode: StatusCodes.Status404NotFound);
            }

            catch (Exception ex)
            {
                return ActionResultHelper.ToBadRequestActionResult(message: ex.Message, title: "Error", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
