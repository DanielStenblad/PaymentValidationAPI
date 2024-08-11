using PaymentValidationAPI.Enums;
using PaymentValidationAPI.Extensions.Common;
using PaymentValidationAPI.Models;
using PaymentValidationAPI.Models.CreditCard;
using PaymentValidationAPI.Services.Common;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PaymentValidationAPI.Services
{
    public static class CreditCardService
    {
        public static Response<CreditCardResponse> ValidateCreditCard(CreditCardRequest request)
        {
            var response = new Response<CreditCardResponse>();

            if (request == null)
            {
                response.Errors.Add("Request is required.", null);
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.CardOwner))
            {
                response.Errors.Add("Card owner is required.", nameof(request.CardOwner));
            }

            if (!IsValidCardNumber(request.CardNumber, out CreditCardType cardType, out string cardNumberError))
            {
                response.Errors.Add(cardNumberError, nameof(request.CardNumber));
            }

            if (!IsValidCVC(request.CVC, cardType, out string cvcError))
            {
                response.Errors.Add(cvcError, nameof(request.CVC));
            }

            if (!IsValidExpireDate(request.ExpireDate, out string expireDateError))
            {
                response.Errors.Add(expireDateError, nameof(request.ExpireDate));
            }

            if (response.IsValid)
                response.Data = new CreditCardResponse(cardType.ToDescriptionString());

            return response;
        }

        private static bool IsValidCardNumber(string cardNumber, out CreditCardType creditCardType, out string error)
        {
            error = null;
            creditCardType = CreditCardType.Unknown;

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                error = "Card number is required.";
                return false;
            }

            string cleanedCardNumber = Regex.Replace(cardNumber, @"\D", "");

            if (!string.IsNullOrEmpty(cleanedCardNumber))
            {
                if (ValidationService.IsValidLuhn(cleanedCardNumber))
                {
                    creditCardType = GetCardType(cleanedCardNumber);

                    if (creditCardType == CreditCardType.Unknown)
                        error = "Unsupported card type.";
                    else
                        return true;
                }
            }

            if (error == null)
                error = "Invalid card number.";

            return false;
        }

        private static bool IsValidExpireDate(string expireDate, out string error)
        {
            error = null;
            var format = "MM/yy";

            if (string.IsNullOrWhiteSpace(expireDate))
                error = "Expire date is required.";
            else if (!DateTime.TryParseExact(expireDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedExpireDate))
                error = $"Invalid expire date. Expected format: {format}";
            else if (parsedExpireDate < DateTime.UtcNow)
                error = "Card has expired.";

            return error == null;
        }

        private static bool IsValidCVC(string cvc, CreditCardType creditCardType, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(cvc))
            {
                error = "CVC is required.";
                return false;
            }

            if (creditCardType != CreditCardType.Unknown)
            {
                if (creditCardType == CreditCardType.AmericanExpress)
                {
                    if (!Regex.IsMatch(cvc, @"^\d{4}$"))
                        error = "Invalid CVC. American Express requires 4 digits.";
                }
                else if (!Regex.IsMatch(cvc, @"^\d{3}$"))
                {
                    error = "Invalid CVC.";
                }
            }

            return error == null;
        }

        private static CreditCardType GetCardType(string cardNumber)
        {
            if (Regex.IsMatch(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$"))
            {
                return CreditCardType.Visa;
            }
            else if (Regex.IsMatch(cardNumber, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$"))
            {
                return CreditCardType.MasterCard;
            }
            else if (Regex.IsMatch(cardNumber, @"^3[47][0-9]{13}$"))
            {
                return CreditCardType.AmericanExpress;
            }

            return CreditCardType.Unknown;
        }
    }
}
