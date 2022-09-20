using Mastermind;
using Mastermind.Presentation.InputOutput;
using Moq;
using Xunit;

namespace MastermindTests;

public class ControllerTests
{
    [Fact]
    public void GivenAController_WhenTheGameIsInitialised_ThenTheWelcomeGameMessagesShouldBeDisplayed()
    {
        //Arrange
        var mockedConsole = new Mock<IInputOutput>();
        mockedConsole.Setup(console => console.OutputWelcomeMessage())
            .Verifiable();
        var gameController = new Controller(mockedConsole.Object);

        // Act
        gameController.Initialise();

        // Assert
        mockedConsole.Verify();
    }
}