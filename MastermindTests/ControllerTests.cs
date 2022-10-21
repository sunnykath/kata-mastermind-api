using System.Collections.Generic;
using Mastermind;
using Mastermind.Enums;
using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;
using Moq;
using Xunit;

namespace MastermindTests;

public class ControllerTests
{
    private readonly Mock<IRandomizer> _mockRandomizer;
    private readonly Mock<IInputOutput> _mockConsole;
    public ControllerTests()
    {
        _mockConsole = new Mock<IInputOutput>();
        _mockRandomizer = new Mock<IRandomizer>();
    }
    
    [Fact]
    public void GivenAController_WhenTheGameIsPlayed_ThenTheWelcomeGameMessagesShouldBeDisplayed()
    {
        //Arrange
        _mockConsole.Setup(console => console.OutputWelcomeMessage())
            .Verifiable();
        var gameController = new Controller(_mockConsole.Object);

        // Act
        gameController.PlayGame(_mockRandomizer.Object);

        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void GivenTheGameHasBeenInitialised_WhenPlayGameIsCalledAndTheUserIsPromptedToEnterInAGuess_ThenTheGuessShouldBePrintedOut()
    {
        //Arrange
        var guessedColours = new[]
            { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare }; 
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = guessedColours
            });
        _mockConsole.Setup(output => output.OutputColourArray(guessedColours))
            .Verifiable();

        _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
            .Returns(new [] { Colour.Blue , Colour.Red, Colour.Orange, Colour.Purple});
        _mockRandomizer.Setup(randomizer =>
                randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns<List<Clue>>(clues => clues);
        
        var gameController = new Controller(_mockConsole.Object);
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);

        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void
        GivenTheGameHasBeenInitialised_WhenPlayGameIsCalledAndTheUserEntersAGuess_ThenTheCluesBasedOnTheUserGuessShouldBeDisplayed()
    {
        //Arrange
        var guessedColours = new[]
            { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare };
        var printedClues = new List<string> { Constants.BlackSquare, Constants.WhiteSquare };
        
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = guessedColours
            });
        _mockConsole.Setup(output => output.OutputClues(printedClues))
            .Verifiable();
        
        _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
            .Returns(new [] { Colour.Blue , Colour.Red, Colour.Orange, Colour.Purple});
        _mockRandomizer.Setup(randomizer =>
                randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns<List<Clue>>(clues => clues)
            .Verifiable();
        
        var gameController = new Controller(_mockConsole.Object);
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);
    
        // Assert
        _mockConsole.Verify();
        _mockRandomizer.Verify();
    }
    
    [Fact]
    public void
        GivenTheGameHasBeenInitialised_WhenPlayGameIsCalledAndTheUserEntersAIncorrectGuess_ThenTheUserShouldBePromptedToEnterAnotherGuess()
    {
        //Arrange
        var guessedColours = new[]
            { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare };
        var printedClues = new List<string> { Constants.BlackSquare, Constants.WhiteSquare };

        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = guessedColours
            });
        _mockConsole.Setup(output => output.OutputClues(printedClues))
            .Verifiable();
        
        _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
            .Returns(new [] { Colour.Blue , Colour.Red, Colour.Orange, Colour.Purple});
        _mockRandomizer.Setup(randomizer =>
                randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns<List<Clue>>(clues => clues)
            .Verifiable();
        
        var gameController = new Controller(_mockConsole.Object);
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);
    
        // Assert
        _mockConsole.Verify(console => console.GetAGuessInput(), Times.AtLeast(2));
        _mockRandomizer.Verify();
    }
}