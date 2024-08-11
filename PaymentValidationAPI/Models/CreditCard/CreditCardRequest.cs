namespace PaymentValidationAPI.Models.CreditCard
{
    /// <summary>
    /// The information found on the credit card
    /// </summary>
    /// <param name="CardOwner">The name of the card owner. Visible on the front of the card.</param>   
    /// <param name="CardNumber">The card number. Visible on the front of the card.</param>
    /// <param name="ExpireDate">The month and year when the credit card expires (Expected format 'MM/yy'). Visible on the front of the card.</param>
    /// <param name="CVC">The 3-4 digits found on the back of the card.</param>
    public record CreditCardRequest(        
        string CardOwner,
        string CardNumber,        
        string ExpireDate,
        string CVC);
}
