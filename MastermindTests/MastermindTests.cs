using System.Collections.Generic;
using Mastermind.Application.BusinessRules;
using Mastermind.Application.Randomizer;
using Mastermind.Domain;
using Mastermind.Presentation.InputOutput;
using Moq;
using Xunit;

namespace MastermindTests;

public class MastermindTests
{
    private readonly Mock<IRandomizer> _mockRandomizer;
    private readonly Mock<IInputOutput> _mockConsole;
    private readonly Mastermind.Presentation.Mastermind _mastermind;

    private readonly Colour[] _dummyAnswer = { Colour.Blue, Colour.Red, Colour.Orange, Colour.Purple };
    private readonly string[] _dummyIncorrectGuess = { ColourSquares.Blue, ColourSquares.Green, ColourSquares.Yellow, ColourSquares.Red};
    
    public MastermindTests()
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

        _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(GameConstants.SelectedNumberOfColours))
            .Returns(_dummyAnswer);
        
        _mockRandomizer.Setup(randomizer =>
                randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
            .Returns<List<Clue>>(clues => clues)
            .Verifiable();        
        _mastermind = new Mastermind.Presentation.Mastermind(_mockConsole.Object, _mockRandomizer.Object);

    }
    
    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserQuitsTheGame_ThenTheGameQuitScreenShouldBeDisplayed()
    {
        //Arrange
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                HasQuit = true
            });

        _mockConsole.Setup(input => input.OutputGameQuitMessage())
            .Verifiable();
        
        // Act
        _mastermind.PlayGame();
    
        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void GivenPlayGameHasBeenCalled_WhenTheUserIsPromptedToEnterInAGuess_ThenTheGuessShouldBePrintedOut()
    {
        //Arrange
        _mockConsole.Setup(output => output.OutputColours(_dummyIncorrectGuess))
            .Verifiable();
        
        // Act
        _mastermind.PlayGame();

        // Assert
        _mockConsole.Verify();
    }

    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserEntersAGuess_ThenTheCluesBasedOnTheUserGuessShouldBeDisplayed()
    {
        //Arrange
        var printedClues = new List<string> { ColourSquares.Black, ColourSquares.White};
        
        _mockConsole.Setup(output => output.OutputClues(printedClues))
            .Verifiable();

        // Act
        _mastermind.PlayGame();
    
        // Assert
        _mockConsole.Verify();
        _mockRandomizer.Verify();
    }
    
    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserEntersAnIncorrectGuess_ThenTheUserShouldBePromptedToEnterAnotherGuess()
    {
        // Act
        _mastermind.PlayGame();
    
        // Assert
        _mockConsole.Verify(console => console.GetAGuessInput(), Times.AtLeast(2));
    }
    
    [Fact]
    public void
        GivenPlayGameIsCalled_WhenTheUserEntersACorrectGuess_ThenTheWinningScreenShouldBeDisplayed()
    {
        //Arrange
        var dummyCorrectGuess = new [] { ColourSquares.Blue, ColourSquares.Red, ColourSquares.Orange, ColourSquares.Purple};
        
        _mockConsole.Setup(input => input.GetAGuessInput())
            .Returns(new PlayerInput
            {
                ColoursInput = dummyCorrectGuess
            });

        _mockConsole.Setup(input => input.OutputGameWonMessage())
            .Verifiable();
        
        // Act
        _mastermind.PlayGame();
    
        // Assert
        _mockConsole.Verify();
    }
}