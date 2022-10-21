using System.Collections.Generic;
using Mastermind;
using Mastermind.Enums;
using Mastermind.GamePlay;
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
            
            mockedInputOutput.Setup(output => output.OutputWelcomeMessage())
                .Verifiable();
        
            // Act
            view.DisplayInitialMessage();
        
            // Assert
            mockedInputOutput.Verify();
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheGetUpdatedUserGuessIsCalledAfterUpdatePlayerGuess_ThenShouldReturnAnArrayOfColoursContainingTheUserGuess()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            var expectedGuess = new [] {Colour.Red, Colour.Blue, Colour.Yellow, Colour.Green};
            
            mockedInputOutput.Setup(output => output.GetAGuessInput())
                .Returns(new PlayerInput
                { 
                    ColoursInput = new[] {Constants.RedSquare, Constants.BlueSquare, Constants.YellowSquare, Constants.GreenSquare}
                })
                .Verifiable();
        
            // Act
            view.UpdatePlayerGuess();
            var actualGuess = view.GetUpdatedUserGuess();
        
            // Assert
            mockedInputOutput.Verify();
            Assert.Equal(expectedGuess, actualGuess);
        }
        
        [Fact]
        public void GivenAViewInstanceWithAConsoleDependency_WhenTheDisplayCluesIsCalled_ThenShouldDisplayAStringWithAllTheClues()
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            var clueInput = new List<Clue> {Clue.Black, Clue.White, Clue.Black};
            mockedInputOutput.Setup(output => 
                    output.OutputClues(new List<string> {Constants.BlackSquare, Constants.WhiteSquare, Constants.BlackSquare}))
                .Verifiable();
        
            // Act
            view.DisplayClues(clueInput);
        
            // Assert
            mockedInputOutput.Verify();
        }
        
        [Theory]
        [InlineData(GameStatus.Won)]
        [InlineData(GameStatus.Quit)]
        public void GivenAViewInstanceWithAConsoleDependency_WhenDisplayEndGameResultIsCalled_ThenShouldDisplayTheAnswerWithAFinalGameMessage(GameStatus gameStatus)
        {
            // Arrange
            var mockedInputOutput = new Mock<IInputOutput>();
            var view = new View(mockedInputOutput.Object);
            var correctAnswer = new[] {Colour.Blue, Colour.Red, Colour.Yellow, Colour.Green};

            if (gameStatus == GameStatus.Won)
            {
                mockedInputOutput.Setup(output => 
                    output.OutputGameWonMessage())
                .Verifiable();
            }
            else
            {
                mockedInputOutput.Setup(output => 
                        output.OutputGameQuitMessage())
                    .Verifiable();
            }
            mockedInputOutput.Setup(output => 
                    output.OutputColourArray(new []{Constants.BlueSquare, Constants.RedSquare, Constants.YellowSquare, Constants.GreenSquare}))
                .Verifiable();
        
            // Act
            view.DisplayEndGameResult(gameStatus, new Game()
            {
                GuessingCount = 34,
                SelectedColours = correctAnswer
            });
        
            // Assert
            mockedInputOutput.Verify();
        }
    }
}