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
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGameIsStarted_ThenTheConsoleShouldDisplayGameInformation()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            
            mockedInputOutput.Setup(output => output.PrintOutput(Constants.Title))
                .Verifiable();
            mockedInputOutput.Setup(output => output.PrintOutput(Constants.GameInformation))
                .Verifiable();

            // Act
            view.StartGame();

            // Assert
            mockedInputOutput.Verify();
        }
    }
}