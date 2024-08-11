namespace PaymentValidationAPI.Services.Common
{
    public static class ValidationService
    {
        public static bool IsValidLuhn(string cardNumber)
        {
            return IsValidLuhn(cardNumber.Select(c => c - '0').ToArray());
        }

        public static bool IsValidLuhn(int[] digits)
        {
            int checkDigit = 0;
            for (int i = digits.Length - 2; i >= 0; --i)
            {
                checkDigit +=
                    (i & 1) == (digits.Length & 1)
                        ? digits[i] > 4 ? digits[i] * 2 - 9 : digits[i] * 2
                        : digits[i];
            }

            return (10 - checkDigit % 10) % 10 == digits[^1];
        }
    }
}
