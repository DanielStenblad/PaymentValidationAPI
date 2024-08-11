using PaymentValidationAPI.Enums;
using PaymentValidationAPI.Models.CreditCard;
using PaymentValidationAPI.Services;
using PaymentValidationAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using PaymentValidationAPI.Extensions.Common;
using System.Net;
using Swashbuckle.AspNetCore.Filters;
using PaymentValidationAPI.Models.CreditCard.Examples;
using PaymentValidationAPI.Services.Common;

public static class CreditCardApiExtensions
{
    public static IServiceCollection AddCreditCardEndpoints(this IServiceCollection services)
    {        
        return services;
    }

    public static WebApplication MapCreditCardEndpoints(this WebApplication app)
    {
        app.MapPost("/creditcard",
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CreditCardResponse200Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CreditCardResponse400Example))]
        (CreditCardRequest request) =>
        {
            return ResponseService.BuildResponse(CreditCardService.ValidateCreditCard(request));
        })
        .WithTags(OpenApiTagType.PaymentMethods.ToDescriptionString())
        .WithSummary("Validates a credit card")        
        .WithMetadata(new SwaggerResponseAttribute(200, "<b>OK</b> - Returns the credit card type.", typeof(Response<CreditCardResponse>)))
        .WithMetadata(new SwaggerResponseAttribute(400, "<b>Bad request</b> - Returns a list of errors", typeof(Response<string>)))
        .Produces<Response<CreditCardResponse>>((int)HttpStatusCode.OK)
        .Produces<Response<CreditCardResponse>>((int)HttpStatusCode.BadRequest)
        .WithDescription($"This endpoint accepts credit card details as input and returns a validation result. The service checks if the credit card information is valid, verifies the card type (e.g., Visa, MasterCard, American Express), and ensures that the card is not expired.");                    

        return app;
    }
}