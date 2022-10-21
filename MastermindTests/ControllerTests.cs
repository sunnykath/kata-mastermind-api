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

    private readonly Colour[] _dummyAnswer = { Colour.Blue, Colour.Red, Colour.Orange, Colour.Purple };
    private readonly string[] _dummyIncorrectGuess = { Constants.BlueSquare, Constants.GreenSquare, Constants.YellowSquare, Constants.RedSquare };
    
    public ControllerTests()
    {
        _mockConsole = new Mock<IInputOutput>();
        _mockRandomizer = new Mock<IRandomizer>();

        _mockConsole.SetupSequence(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = _dummyIncorrectGuess
            })
            .Returns(new PlayerInput
            {
                HasQuit = true
            });

        _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
            .Returns(_dummyAnswer);
        
        _mockRandomizer.Setup(randomizer =>
                randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns<List<Clue>>(clues => clues)
            .Verifiable();
    }
    
    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserQuitsTheGame_ThenTheGameQuitScreenShouldBeDisplayed()
    {
        //Arrange
        var gameController = new Controller(_mockConsole.Object);
        var dummyCorrectGuess = new [] { Constants.BlueSquare, Constants.RedSquare, Constants.OrangeSquare, Constants.PurpleSquare};
        
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                HasQuit = true
            });

        _mockConsole.Setup(input => input.OutputGameQuitMessage())
            .Verifiable();
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);
    
        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void GivenPlayGameHasBeenCalled_WhenTheUserIsPromptedToEnterInAGuess_ThenTheGuessShouldBePrintedOut()
    {
        //Arrange
        _mockConsole.Setup(output => output.OutputColourArray(_dummyIncorrectGuess))
            .Verifiable();
        
        var gameController = new Controller(_mockConsole.Object);
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);

        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserEntersAGuess_ThenTheCluesBasedOnTheUserGuessShouldBeDisplayed()
    {
        //Arrange
        var printedClues = new List<string> { Constants.BlackSquare, Constants.WhiteSquare };
        
        _mockConsole.Setup(output => output.OutputClues(printedClues))
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
        GivenPlayGameIsCalled_WhenTheUserEntersAnIncorrectGuess_ThenTheUserShouldBePromptedToEnterAnotherGuess()
    {
        //Arrange
        var gameController = new Controller(_mockConsole.Object);
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);
    
        // Assert
        _mockConsole.Verify(console => console.GetAGuessInput(), Times.AtLeast(2));
    }
    
    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserEntersACorrectGuess_ThenTheWinningScreenShouldBeDisplayed()
    {
        //Arrange
        var gameController = new Controller(_mockConsole.Object);
        var dummyCorrectGuess = new [] { Constants.BlueSquare, Constants.RedSquare, Constants.OrangeSquare, Constants.PurpleSquare};
        
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = dummyCorrectGuess
            });

        _mockConsole.Setup(input => input.OutputGameWonMessage())
            .Verifiable();
        
        // Act
        gameController.PlayGame(_mockRandomizer.Object);
    
        // Assert
        _mockConsole.Verify();
    }
}