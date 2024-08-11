namespace PaymentValidationAPI.Models.CreditCard
{
    /// <summary>
    /// Credit card response class
    /// </summary>
    /// <param name="CardType">The type of the credit card based the details from the CreditCardRequest</param>
    public record CreditCardResponse(string CardType);    
}
