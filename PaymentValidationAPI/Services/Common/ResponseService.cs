using PaymentValidationAPI.Models;

namespace PaymentValidationAPI.Services.Common
{
    public static class ResponseService
    {
        public static IResult BuildResponse<T>(Response<T> response)
        {
            if (response.IsValid)
            {
                return Results.Ok(response);
            }
            else
            {
                return Results.BadRequest(response);
            }
        }
    }
}
