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
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGetUserGuessIsCalled_ThenShouldDisplayAllColoursAndAskForUserInput()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            
            mockedInputOutput.Setup(output => output.DisplayOutput(Constants.GetInputPrompt))
                .Verifiable();
            mockedInputOutput.Setup(output => output.DisplayOutput(Constants.DefaultColourRow))
                .Verifiable();

            // Act
            view.GetUserGuess();

            // Assert
            mockedInputOutput.Verify();
        }
        
    }
}