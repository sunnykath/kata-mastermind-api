using System.Collections.Generic;
using Mastermind.Domain.Models;
using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class ControllerTests
    {
        private readonly Mock<IInputOutput> _mockedInputOutput;
        private readonly Controller _controller;

        public ControllerTests()
        {
            _mockedInputOutput = new Mock<IInputOutput>();
            _controller = new Controller(_mockedInputOutput.Object);

        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayGameDetailsIsCalled_ThenShouldDisplayTitleAndGameInformation()
        {
            // Arrange
            
            _mockedInputOutput.Setup(output => output.OutputWelcomeMessage())
                .Verifiable();
        
            // Act
            _controller.DisplayInitialMessage();
        
            // Assert
            _mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGetUpdatedUserGuessIsCalledAfterUpdatePlayerGuess_ThenShouldReturnAnArrayOfColoursContainingTheUserGuess()
        {
            // Arrange
            var expectedGuess = new [] {Colour.Red, Colour.Blue, Colour.Yellow, Colour.Green};
            
            _mockedInputOutput.Setup(output => output.GetAGuessInput())
                .Returns(new PlayerInput
                { 
                    ColoursInput = new[] {Squares.Red, Squares.Blue, Squares.Yellow, Squares.Green}
                })
                .Verifiable();
        
            // Act
            _controller.UpdatePlayerGuess();
            var actualGuess = _controller.GetUpdatedUserGuess();
        
            // Assert
            _mockedInputOutput.Verify();
            Assert.Equal(expectedGuess, actualGuess);
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayCluesIsCalled_ThenShouldDisplayAStringWithAllTheClues()
        {
            // Arrange
            var view = new Controller(_mockedInputOutput.Object);
            var clueInput = new List<Clue> {Clue.Black, Clue.White, Clue.Black};
            _mockedInputOutput.Setup(output => 
                    output.OutputClues(new List<string> {Squares.Black, Squares.White, Squares.Black}))
                .Verifiable();
        
            // Act
            view.DisplayClues(clueInput);
        
            // Assert
            _mockedInputOutput.Verify();
        }
        
        [Theory]
        [InlineData(GameStatus.Won)]
        [InlineData(GameStatus.Quit)]
        public void GivenAViewInstanceWithAConsoleDependency_WhenDisplayEndGameResultIsCalled_ThenShouldDisplayTheAnswerWithAFinalGameMessage(GameStatus gameStatus)
        {
            // Arrange
            var correctAnswer = new[] {Colour.Blue, Colour.Red, Colour.Yellow, Colour.Green};

            if (gameStatus == GameStatus.Won)
            {
                _mockedInputOutput.Setup(output => 
                    output.OutputGameWonMessage())
                .Verifiable();
            }
            else
            {
                _mockedInputOutput.Setup(output => 
                        output.OutputGameQuitMessage())
                    .Verifiable();
            }
            _mockedInputOutput.Setup(output => 
                    output.OutputColourArray(new []{Squares.Blue, Squares.Red, Squares.Yellow, Squares.Green}))
                .Verifiable();
        
            // Act
            _controller.DisplayEndGameResult(gameStatus, new Game()
            {
                GuessingCount = 34,
                SelectedColours = correctAnswer
            });
        
            // Assert
            _mockedInputOutput.Verify();
        }
    }
}