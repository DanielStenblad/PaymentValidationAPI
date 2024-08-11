namespace PaymentValidationAPI.Models.Common
{
    /// <summary>
    /// Error class to hold error message and field
    /// </summary>
    /// <param name="Message">Error message</param>
    /// <param name="Field">Field name</param>
    public record Error(
        string Message,
        string Field);
}
