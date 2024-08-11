using PaymentValidationAPI.Models;

namespace PaymentValidationAPI.Tests.UnitTests.Common
{
    internal static class ResponseTests
    {
        public static void IsValidTest<T>(Response<T> response)
        {
            Assert.NotNull(response);

            Assert.True(response.IsValid);
            Assert.Empty(response.Errors);
        }

        public static void IsNotValidTest<T>(Response<T> response)
        {
            Assert.NotNull(response);

            Assert.False(response.IsValid);

            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count > 0);

            foreach (var error in response.Errors)            
                Assert.NotNull(error.Message);       
            
            Assert.Null(response.Data);
        }
    }
}
