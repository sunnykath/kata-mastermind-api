using System.Collections.Generic;
using Mastermind;
using Mastermind.Domain.Models;
using Mastermind.Randomizer;
using Xunit;

namespace MastermindTests;

public class ControllerTests
{
    [Fact]
    public void GivenAController_WhenStartNewGameIsCalled_ThenShouldReturnANewGameWithGameStatusPlaying()
    {
        // Arrange 
        var controller = new Controller(new DefaultRandomizer());
        
        // Act 
        var game = controller.StartNewGame();
        
        // Assert
        Assert.Equal(GameStatus.Playing, game.GameState);
    }

    [Fact]
    public void
        GivenAnOnGoingGame_WhenUpdateGameWithLastPlayerGuessIsCalledWithAQuitStatus_ThenShouldNotUpdateTheGameObjectAtAll()
    {
        // Arrange 
        var controller = new Controller(new DefaultRandomizer());
        var selectedColours = new[] { Colour.Red, Colour.Yellow, Colour.Green, Colour.Red };
        // Act 
        var initialGame = new Game()
        {
            SelectedColours = selectedColours,
            LatestPlayerGuess = selectedColours,
            GameState = GameStatus.Quit,
            GuessingCount = 12,
            Clues = new List<Clue>()
        };
        var updatedGame = controller.UpdateGameWithLastPlayerGuess(initialGame);

        // Assert
        Assert.Equal(initialGame, updatedGame);
    }
    
    [Fact]
    public void
        GivenAnOnGoingGame_WhenUpdateGameWithLastPlayerGuessIsCalledWithACorrectLatestPlayerGuess_ThenShouldUpdateTheGameStatusToWon()
    {
        // Arrange 
        var controller = new Controller(new DefaultRandomizer());
        var selectedColours = new[] { Colour.Red, Colour.Yellow, Colour.Green, Colour.Red };
        // Act 
        var game = controller.UpdateGameWithLastPlayerGuess(new Game()
        {
            SelectedColours = selectedColours,
            LatestPlayerGuess = selectedColours,
            GameState = GameStatus.Playing,
            GuessingCount = 12
        });

        // Assert
        Assert.Equal(GameStatus.Won, game.GameState);
    }
}