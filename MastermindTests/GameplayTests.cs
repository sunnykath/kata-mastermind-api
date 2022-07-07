using System;
using System.Linq;
using Mastermind;
using Mastermind.Randomizer;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GameplayTests
    {
        [Fact] 
        public void GivenTheGameIsInitialised_WhenItCreatesTheSelectedColourArray_ThenShouldUseRandomizerToPickFourColours()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var expectedSelectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(expectedSelectedColours)
                .Verifiable();
            
            // Act
            game.Initialise();
            var actualSelectedColours = game.GetSelectedColours();
            
            // Assert
            mockRandomizer.Verify();
            Assert.Equal(expectedSelectedColours, actualSelectedColours);
        }
        
        [Theory]
        [InlineData( new[] {Colour.Green, Colour.Blue, Colour.Orange, Colour.Red}, 3, 1)]
        [InlineData( new[] {Colour.Green, Colour.Blue, Colour.Blue, Colour.Green}, 0, 3)]
        [InlineData( new[] {Colour.Blue, Colour.Blue, Colour.Blue, Colour.Blue}, 0, 2)]
        [InlineData( new[] {Colour.Red, Colour.Blue, Colour.Yellow, Colour.Purple}, 1, 2)]
        [InlineData( new[] {Colour.Purple, Colour.Yellow, Colour.Purple, Colour.Orange}, 0, 0)]
        [InlineData( new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green}, 0, 4)]
        public void GivenTheSelectedColourArrayHasBeenCreated_WhenEvaluateAnswerIsFollowedByTheGetCluesCall_ThenShouldReturnTheCorrectClues(
            Colour[] predictedAnswer,
            int expectedWhiteClueCount,
            int expectedBlackClueCount)
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green});
            
            game.Initialise();
            
            // Act
            game.EvaluatePredictedAnswer(predictedAnswer);
            var clues = game.GetClues();
            
            // Assert
            Assert.Equal(expectedWhiteClueCount, clues.Count(c => c == Clue.White));
            Assert.Equal(expectedBlackClueCount, clues.Count(c => c == Clue.Black));
        }
        
        [Fact]
        public void GivenTheAnswerIsTheSameAsTheSelectedArray_WhenEvaluateAnswerIsFollowedByTheHasWonGameCall_ThenShouldReturnTrue()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
    
            game.Initialise();
    
            // Act
            game.EvaluatePredictedAnswer(selectedColours);
            var hasWonGame = game.HasWonGame();
    
            // Assert
            Assert.True(hasWonGame);
        }

        [Theory]
        [InlineData(new []{Colour.Red, Colour.Blue, Colour.Blue, Colour.Purple, Colour.Orange, Colour.Red, Colour.Yellow})]
        [InlineData(new []{Colour.Red, Colour.Blue, Colour.Blue, Colour.Green, Colour.Blue})]
        [InlineData(new []{Colour.Red, Colour.Blue, Colour.Blue})]
        [InlineData(new Colour[]{ })]
        public void GivenTheAnswerDoesntContainExactlyFourColours_WhenEvaluateAnswerIsCalled_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfColours(Colour[] invalidAnswer)
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);

            game.Initialise();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => game.EvaluatePredictedAnswer(invalidAnswer));
            Assert.Equal(Constants.InvalidNumberOfColoursExceptionMessage, exception.Message); 
        }
        
        [Fact]
        public void GivenTheAnswerIsNotCorrect_WhenEvaluateAnswerIsCalledMoreThanTheMaxNumberOfGuesses_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfTries()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            var incorrectAnswer = new[] {Colour.Red, Colour.Orange, Colour.Blue, Colour.Yellow};
            
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
            game.Initialise();
    
            // Act
            for (var i = 0; i < Constants.MaxNumberOfGuesses; i++)
            {
                game.EvaluatePredictedAnswer(incorrectAnswer);
            }

            // Assert
            var exception = Assert.Throws<Exception>(() => game.EvaluatePredictedAnswer(incorrectAnswer));
            Assert.Equal(Constants.TooManyTriesExceptionMessage, exception.Message);
        }
    }
}