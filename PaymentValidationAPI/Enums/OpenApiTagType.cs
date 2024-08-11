using System.ComponentModel;

namespace PaymentValidationAPI.Enums
{
    public enum OpenApiTagType
    {
        Unknown,

        [Description("Payment Methods")]        
        PaymentMethods,        
    }
}
