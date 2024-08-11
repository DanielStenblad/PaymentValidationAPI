using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using PaymentValidationAPI.Models;
using PaymentValidationAPI.Models.Common;

namespace PaymentValidationAPI.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var code = HttpStatusCode.InternalServerError;

            var response = new Response<string>()
            {                
                Errors = new List<Error>()
                {
                    new Error(Message: "An unexpected error has occurred.", Field: null)
                }
            };            

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
