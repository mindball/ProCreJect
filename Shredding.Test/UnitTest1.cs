using Xunit;

namespace Shredding.Test
{
    public class UnitTest1
    {
        private readonly string openCurlyBracket = $"{{{0}:";
        private const string closedCurlyBracket = "}";
        private const int fieldOne = 1;
        private const int fieldTwo = 2;
        private const int fieldThree = 3;
        private const int fieldFour = 4;
        private const int fieldFive = 5;
        private string message  = "{1:content between curly bracket}";

        [Fact]
        public void StringExtensionMethodBetweenShoudReturnEmptyString_WhenMessageIsNullOrEmpty()
        {
            //Arrange
            message = string.Empty;
            string expected = string.Empty;
            string expectedTwo = string.Empty;
           
            //Act
            var actual = message.Between(openCurlyBracket, closedCurlyBracket);
            message = null;
            var actualTwo = message.Between(openCurlyBracket, closedCurlyBracket);
           
            //Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedTwo, actualTwo);
        }

        [Fact]
        public void StringExtensionMethodBetweenShoudReturnEmptyString_WhenMessageDoesNotHaveClosingCurlyBracket()
        {
            //Arrange
            message = "{1:content between curly bracket";
            string expected = string.Empty;            

            //Act
            var actual = message.Between(openCurlyBracket, closedCurlyBracket);            

            //Assert
            Assert.Equal(expected, actual);            
        }

        [Fact]
        public void StringExtensionMethodBetweenShoudReturnEmptyString_WhenMessageHaveReverseOrderOfBracket()
        {
            //Arrange
            message = "1:content between curly bracket}{0:";
            string expected = string.Empty;

            //Act
            var actual = message.Between(openCurlyBracket, closedCurlyBracket);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StringExtensionMethodBetweenShoudReturnContentString_WhenMessageHaveTwoClosingracket()
        {
            //Arrange
            string expected = "test";
            message = $"1:content between curly bracket}}{{0:{expected}}}";            

            //Act
            var actual = message.Between(openCurlyBracket, closedCurlyBracket);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StringExtensionMethodBetweenShoudReturnEmptyString_WhenMessageHasMatchButNoContent()
        {
            //Arrange
            string expected = string.Empty;
            message = "{0:}";

            //Act
            var actual = message.Between(openCurlyBracket, closedCurlyBracket);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
