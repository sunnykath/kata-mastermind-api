using Mastermind.Presentation.InputOutput;
using Xunit;

namespace MastermindTests;

public class InputValidationTests
{
    [Fact]
    public void GivenValidateInputIsCalledOnAString_WhenTheStringIsSimplyQ_ThenShouldReturnTrue()
    {
        // Arrange
        var validator = new ConsoleInputValidator();

        // Act
        var isValid = validator.ValidateInput("q");

        // Assert
        Assert.True(isValid);
    }
    
    [Theory]
    [InlineData("w324", false)]
    [InlineData("12d1", false)]
    [InlineData("qwd1", false)]
    [InlineData("verb", false)]
    public void GivenValidateInputIsCalledOnAString_WhenTheStringContainsNonNumbersExceptJustQ_ThenShouldReturnFalse(string inputString, bool expectedIsValid)
    {
        // Arrange
        var validator = new ConsoleInputValidator();
    
        // Act
        var actualIsValid = validator.ValidateInput(inputString);
    
        // Assert
        Assert.Equal(expectedIsValid, actualIsValid);
    }
    
    [Theory]
    [InlineData("1234", true)]
    [InlineData("12", false)]
    [InlineData("123", false)]
    [InlineData("12345", false)]
    [InlineData("1234567890", false)]
    public void GivenValidateInputIsCalledOnAString_WhenTheStringContainsExactly4Characters_ThenShouldReturnTrueOtherwiseFalse(string inputString, bool expectedIsValid)
    {
        // Arrange
        var validator = new ConsoleInputValidator();

        // Act
        var actualIsValid = validator.ValidateInput(inputString);

        // Assert
        Assert.Equal(expectedIsValid, actualIsValid);
    }
    
    [Theory]
    [InlineData("1239")]
    [InlineData("6784")]
    [InlineData("1209")]
    [InlineData("7890")]
    public void GivenValidateInputIsCalledOnAString_WhenTheStringContainsDigitsLessThanZeroOrMoreThanFive_ThenShouldReturnFalse(string inputString)
    {
        // Arrange
        var validator = new ConsoleInputValidator();

        // Act
        var isValid = validator.ValidateInput(inputString);

        // Assert
        Assert.False(isValid);
    }
    
}