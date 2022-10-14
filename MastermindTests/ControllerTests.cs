using System.Collections.Generic;
using Mastermind;
using Mastermind.Enums;
using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;
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

    [Fact]
    public void GivenTheGameHasBeenInitialised_WhenPlayGameIsCalledAndTheUserIsPromptedToEnterInAGuess_ThenTheGuessShouldBePrintedOut()
    {
        //Arrange
        var guessedColours = new[]
            { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare }; 
        var mockedConsole = new Mock<IInputOutput>();
        mockedConsole.Setup(input => input.GetAGuessInput())
            .Returns(guessedColours);
        mockedConsole.Setup(output => output.OutputColourArray(guessedColours))
            .Verifiable();
        var gameController = new Controller(mockedConsole.Object);
        gameController.Initialise();

        var mockedRandomizer = new Mock<IRandomizer>();
        mockedRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
            .Returns(new [] { Colour.Blue , Colour.Red, Colour.Orange, Colour.Purple});
        
        // Act
        gameController.PlayGame(mockedRandomizer.Object);

        // Assert
        mockedConsole.Verify();
    }

    [Fact]
    public void
        GivenTheGameHasBeenInitialised_WhenPlayGameIsCalledAndTheUserEntersAGuess_ThenTheCluesBasedOnTheUserGuessShouldBeDisplayed()
    {
        //Arrange
        var guessedColours = new[]
            { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare };
        var printedClues = new List<string> { Constants.BlackSquare, Constants.WhiteSquare };
        var mockedConsole = new Mock<IInputOutput>();
        mockedConsole.Setup(input => input.GetAGuessInput())
            .Returns(guessedColours);
        mockedConsole.Setup(output => output.OutputClues(printedClues))
            .Verifiable();
        var gameController = new Controller(mockedConsole.Object);
        gameController.Initialise();
    
        var randomizedClues = new List<Clue> { Clue.Black, Clue.White };
    
        var mockedRandomizer = new Mock<IRandomizer>();
        mockedRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
        .Returns(new [] { Colour.Blue , Colour.Red, Colour.Orange, Colour.Purple});
        mockedRandomizer.Setup(randomizer => randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns(randomizedClues)
            .Verifiable();
        
        // Act
        gameController.PlayGame(mockedRandomizer.Object);
    
        // Assert
        mockedConsole.Verify();
        mockedRandomizer.Verify();
    }
}