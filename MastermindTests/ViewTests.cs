using Mastermind;
using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class ViewTests
    {
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayGameDetailsIsCalled_ThenShouldDisplayTitleAndGameInformation()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            
            mockedInputOutput.Setup(output => output.DisplayOutput(Constants.Title))
                .Verifiable();
            mockedInputOutput.Setup(output => output.DisplayOutput(Constants.GameInformation))
                .Verifiable();

            // Act
            view.DisplayGameDetails();

            // Assert
            mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGetUserGuessIsCalled_ThenShouldReturnAnArrayOfColoursContainingTheUserGuess()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            var expectedGuess = new [] {Colour.Red, Colour.Blue, Colour.Yellow, Colour.Green};
            
            mockedInputOutput.Setup(output => output.GetPlayerInput())
                .Returns(new[] {Constants.RedSquare, Constants.BlueSquare, Constants.YellowSquare, Constants.GreenSquare})
                .Verifiable();

            // Act
            var actualGuess = view.GetUserGuess();

            // Assert
            mockedInputOutput.Verify();
            Assert.Equal(expectedGuess, actualGuess);
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayCluesIsCalled_ThenShouldDisplayAStringWithAllTheClues()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            var clueInput = new[] {Clue.Black, Clue.White, Clue.Black};
            mockedInputOutput.Setup(output => 
                    output.DisplayOutput($"{Constants.BlackSquare} {Constants.WhiteSquare} {Constants.BlackSquare} {Constants.EmptySquare} \n"))
                .Verifiable();

            // Act
            view.DisplayClues(clueInput);

            // Assert
            mockedInputOutput.Verify();
        }
    }
}