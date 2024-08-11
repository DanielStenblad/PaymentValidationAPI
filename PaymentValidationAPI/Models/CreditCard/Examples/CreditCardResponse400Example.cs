using PaymentValidationAPI.Extensions.Common;
using Swashbuckle.AspNetCore.Filters;

namespace PaymentValidationAPI.Models.CreditCard.Examples
{
    public class CreditCardResponse400Example : IExamplesProvider<Response<CreditCardResponse>>
    {
        public Response<CreditCardResponse> GetExamples()
        {
            var response = new Response<CreditCardResponse>();            
            response.Errors.Add("Invalid credit card number", "CardNumber");            
            return response;
        }
    }    
}
