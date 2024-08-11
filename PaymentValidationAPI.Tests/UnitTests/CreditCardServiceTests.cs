using PaymentValidationAPI.Enums;
using PaymentValidationAPI.Models.Common;
using PaymentValidationAPI.Models.CreditCard;
using PaymentValidationAPI.Services;
using PaymentValidationAPI.Tests.UnitTests.Common;

namespace PaymentValidationAPI.Tests.UnitTests
{
    public class CreditCardServiceTests
    {
        [Fact]
        public void ValidateCreditCard_Visa_ReturnsValid()
        {
            //Arrange
            var expectedCardType = "Visa";
            var request = GetCreditCardRequest(CreditCardType.Visa);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsValidTest(response);
            Assert.NotNull(response.Data);
            Assert.Equal(expectedCardType, response.Data.CardType);
        }

        [Fact]
        public void ValidateCreditCard_MasterCard_ReturnsValid()
        {
            //Arrange
            var expectedCardType = "Master Card";
            var request = GetCreditCardRequest(CreditCardType.MasterCard);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsValidTest(response);
            Assert.NotNull(response.Data);
            Assert.Equal(expectedCardType, response.Data.CardType);
        }

        [Fact]
        public void ValidateCreditCard_AmericanExpress_ReturnsValid()
        {
            //Arrange
            var expectedCardType = "American Express";
            var request = GetCreditCardRequest(CreditCardType.AmericanExpress);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsValidTest(response);
            Assert.NotNull(response.Data);
            Assert.Equal(expectedCardType, response.Data.CardType);
        }

        [Fact]
        public void ValidateCreditCard_Discover_ReturnsInvalid()
        {
            //Arrange            
            var expectedNumberOfErrors = 1;            
            var expectedError = new Error("Unsupported card type.", "CardNumber");

            var request = GetCreditCardRequest(CreditCardType.Unknown);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);
            
            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_MissingRequest_ReturnsInvalid()
        {
            //Arrange            
            var expectedNumberOfErrors = 1;            
            var expectedError = new Error("Request is required.", null);

            CreditCardRequest request = null;

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_MissingFields_ReturnsInvalid()
        {
            //Arrange                        
            var expectedErrors = new List<Error> 
            {
                new Error("Card owner is required.", "CardOwner"),
                new Error("Card number is required.", "CardNumber"),
                new Error("CVC is required.", "CVC"),
                new Error("Expire date is required.", "ExpireDate"),                
            };
            
            var request = new CreditCardRequest("","","","");

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);
            
            Assert.Equal(expectedErrors.Count, response.Errors.Count);
            Assert.Equal(expectedErrors, response.Errors);            
        }

        [Fact]
        public void ValidateCreditCard_MissingCVC_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("CVC is required.", "CVC");

            var example = GetCreditCardRequest(CreditCardType.Visa);

            var request = new CreditCardRequest(
                CardNumber: example.CardNumber,
                ExpireDate: example.ExpireDate,
                CardOwner: example.CardOwner,
                CVC: "");

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);            
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidCVC_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Invalid CVC.", "CVC");

            var example = GetCreditCardRequest(CreditCardType.MasterCard);

            var request = new CreditCardRequest(
                CardNumber: example.CardNumber,
                ExpireDate: example.ExpireDate,
                CardOwner: example.CardOwner,
                CVC: "1234");

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidCVC_AmericanExpess_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Invalid CVC. American Express requires 4 digits.", "CVC");

            var example = GetCreditCardRequest(CreditCardType.AmericanExpress);

            var request = new CreditCardRequest(
                CardNumber: example.CardNumber,
                ExpireDate: example.ExpireDate,
                CardOwner: example.CardOwner,
                CVC: "123");

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidCardNumber_NonNumeric_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Invalid card number.", "CardNumber");

            var example = GetCreditCardRequest(CreditCardType.MasterCard);

            var request = new CreditCardRequest(
                CardNumber: "qwerty",
                ExpireDate: example.ExpireDate,
                CardOwner: example.CardOwner,
                CVC: example.CVC);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidCardNumber_Luhn_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Invalid card number.", "CardNumber");

            var example = GetCreditCardRequest(CreditCardType.MasterCard);

            var request = new CreditCardRequest(
                CardNumber: "123456",
                ExpireDate: example.ExpireDate,
                CardOwner: example.CardOwner,
                CVC: example.CVC);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidExpireDate_Format_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Invalid expire date. Expected format: MM/yy", "ExpireDate");

            var example = GetCreditCardRequest(CreditCardType.MasterCard);

            var request = new CreditCardRequest(
                CardNumber: example.CardNumber,
                ExpireDate: "07/2024",
                CardOwner: example.CardOwner,
                CVC: example.CVC);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        [Fact]
        public void ValidateCreditCard_InvalidExpireDate_Expired_ReturnsInvalid()
        {
            //Arrange                        
            var expectedNumberOfErrors = 1;
            var expectedError = new Error("Card has expired.", "ExpireDate");

            var example = GetCreditCardRequest(CreditCardType.MasterCard);

            var request = new CreditCardRequest(
                CardNumber: example.CardNumber,
                ExpireDate: "07/24",
                CardOwner: example.CardOwner,
                CVC: example.CVC);

            //Act
            var response = CreditCardService.ValidateCreditCard(request);

            //Assert
            ResponseTests.IsNotValidTest(response);

            Assert.Equal(expectedNumberOfErrors, response.Errors.Count);
            Assert.Equal(expectedError, response.Errors.FirstOrDefault());
        }

        private CreditCardRequest GetCreditCardRequest(CreditCardType cardType)
        {
            switch (cardType)
            {
                case CreditCardType.Visa:
                    return new CreditCardRequest(CardOwner: "John Doe", CardNumber: "4111111111111111", ExpireDate: "12/25", CVC: "123");
                case CreditCardType.MasterCard:
                    return new CreditCardRequest(CardOwner: "Jane Smith", CardNumber: "5555555555554444", ExpireDate: "11/26", CVC: "456");
                case CreditCardType.AmericanExpress:
                    return new CreditCardRequest(CardOwner: "Alice Johnson", CardNumber: "378282246310005", ExpireDate: "10/27", CVC: "7890");
                default:
                    return new CreditCardRequest(CardOwner: "Bob Brown", CardNumber: "6011111111111117", ExpireDate: "09/28", CVC: "321"); // Discover
            }
        }
    }
}