using Swashbuckle.AspNetCore.Filters;

namespace PaymentValidationAPI.Models.CreditCard.Examples
{
    public class CreditCardResponse200Example : IExamplesProvider<Response<CreditCardResponse>>
    {
        public Response<CreditCardResponse> GetExamples()
        {
            return new Response<CreditCardResponse>()
            {
                Data = new CreditCardResponse("Visa"),                
            };
        }
    }    
}
