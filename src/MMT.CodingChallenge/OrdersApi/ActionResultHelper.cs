using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi
{
    public static class ActionResultHelper
    {
        public static ActionResult ToBadRequestActionResult(
            string message = "Invalid Request",
            string title = "Invalid request",
            int statusCode = StatusCodes.Status400BadRequest)
        {
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Detail = message
            };
            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/problem+json", "application/problem+xml" }
            };
        }

        public class ProblemDetails
        {
            public string Detail { get; set; }
            public string Title { get; set; }
        }
    }
}
