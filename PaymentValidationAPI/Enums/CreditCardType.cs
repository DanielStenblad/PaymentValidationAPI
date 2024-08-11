using System.ComponentModel;

namespace PaymentValidationAPI.Enums
{
    public enum CreditCardType
    {
        Unknown,
        
        [Description("Visa")]
        Visa,
        
        [Description("Master Card")]
        MasterCard,

        [Description("American Express")]
        AmericanExpress,
    }
}
