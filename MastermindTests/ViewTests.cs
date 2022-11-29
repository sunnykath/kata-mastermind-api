using System.Collections.Generic;
using Mastermind;
using Mastermind.Domain.Models;
using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class ViewTests
    {
        private readonly Mock<IInputOutput> _mockedInputOutput;
        private readonly View _view;

        public ViewTests()
        {
            _mockedInputOutput = new Mock<IInputOutput>();
            _view = new View(_mockedInputOutput.Object);

        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayGameDetailsIsCalled_ThenShouldDisplayTitleAndGameInformation()
        {
            // Arrange
            
            _mockedInputOutput.Setup(output => output.OutputWelcomeMessage())
                .Verifiable();
        
            // Act
            _view.DisplayInitialMessage();
        
            // Assert
            _mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGetUpdatedUserGuessIsCalledAfterUpdatePlayerGuess_ThenShouldReturnAnArrayOfColoursContainingTheUserGuess()
        {
            // Arrange
            var expectedGuess = new [] {Colour.Red, Colour.Blue, Colour.Yellow, Colour.Green};
            var game = new Game();
            
            _mockedInputOutput.Setup(output => output.GetAGuessInput())
                .Returns(new PlayerInput
                { 
                    ColoursInput = new[] {ColourSquares.Red, ColourSquares.Blue, ColourSquares.Yellow, ColourSquares.Green}
                })
                .Verifiable();
        
            // Act
            _view.UpdateLastPlayerGuessInGame(game);
            var actualGuess = game.LatestPlayerGuess;
        
            // Assert
            _mockedInputOutput.Verify();
            Assert.Equal(expectedGuess, actualGuess);
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayGameInfoIsCalled_ThenShouldDisplayAStringWithAllTheClues()
        {
            // Arrange
            var view = new View(_mockedInputOutput.Object);
            var clueInput = new List<Clue> {Clue.Black, Clue.White, Clue.Black};
            _mockedInputOutput.Setup(output => 
                    output.OutputClues(new List<string> {ColourSquares.Black, ColourSquares.White, ColourSquares.Black}))
                .Verifiable();
        
            // Act
            view.DisplayGameInfo(new Game()
            {
                GameState = GameStatus.Playing,
                Clues = clueInput,
                GuessingCount = 59,
                LatestPlayerGuess = new [] { Colour.Blue }
            });
        
            // Assert
            _mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenDisplayGameInfoIsCalledWithGameStatusAsQuit_ThenShouldDisplayTheAnswerWithAFinalGameQuitMessage()
        {
            // Arrange
            var correctAnswer = new[] {Colour.Blue, Colour.Red, Colour.Yellow, Colour.Green};
            _mockedInputOutput.Setup(output => 
                    output.OutputGameQuitMessage())
                .Verifiable();
            _mockedInputOutput.Setup(output => 
                    output.OutputColours(new []{ColourSquares.Blue, ColourSquares.Red, ColourSquares.Yellow, ColourSquares.Green}))
                .Verifiable();
        
            // Act
            _view.DisplayGameInfo(new Game
            {
                GuessingCount = 34,
                SelectedColours = correctAnswer,
                LatestPlayerGuess = correctAnswer,
                GameState = GameStatus.Quit
            });
        
            // Assert
            _mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenDisplayGameInfoIsCalledWithGameStatusAsWon_ThenShouldDisplayTheAnswerWithAFinalGameWonMessage()
        {
            // Arrange
            var correctAnswer = new[] {Colour.Blue, Colour.Red, Colour.Yellow, Colour.Green};

            _mockedInputOutput.Setup(output =>
                    output.OutputGameWonMessage())
                .Verifiable();
            _mockedInputOutput.Setup(output => 
                output.OutputColours(new []{ColourSquares.Blue, ColourSquares.Red, ColourSquares.Yellow, ColourSquares.Green}))
            .Verifiable();
        
            // Act
            _view.DisplayGameInfo(new Game
            {
                GuessingCount = 34,
                SelectedColours = correctAnswer,
                LatestPlayerGuess = correctAnswer,
                GameState = GameStatus.Won
            });
        
            // Assert
            _mockedInputOutput.Verify();
        }
    }
}