using PaymentValidationAPI.Models.Common;

namespace PaymentValidationAPI.Extensions.Common
{
    public static class ListExtensions
    {
        public static List<Error> Add(this List<Error> errors, string message, string field)
        {
            errors.Add(new Error(message, field));
            return errors;
        }
    }
}
