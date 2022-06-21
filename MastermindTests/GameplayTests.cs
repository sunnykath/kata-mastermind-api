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
            var expectedSelectedColours = new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green};
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
        [InlineData( new[] {Colours.Green, Colours.Blue, Colours.Orange, Colours.Red}, 3, 1)]
        [InlineData( new[] {Colours.Green, Colours.Blue, Colours.Blue, Colours.Green}, 0, 3)]
        [InlineData( new[] {Colours.Blue, Colours.Blue, Colours.Blue, Colours.Blue}, 0, 2)]
        [InlineData( new[] {Colours.Red, Colours.Blue, Colours.Yellow, Colours.Purple}, 1, 2)]
        [InlineData( new[] {Colours.Purple, Colours.Yellow, Colours.Purple, Colours.Orange}, 0, 0)]
        [InlineData( new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green}, 0, 4)]
        public void GivenTheSelectedColourArrayHasBeenCreated_WhenEvaluateAnswerIsFollowedByTheGetCluesCall_ThenShouldReturnTheCorrectClues(
            Colours[] predictedAnswer,
            int expectedWhiteClueCount,
            int expectedBlackClueCount)
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green});
            
            game.Initialise();
            
            // Act
            game.EvaluateAnswer(predictedAnswer);
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
            var selectedColours = new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
    
            game.Initialise();
    
            // Act
            game.EvaluateAnswer(selectedColours);
            var hasWonGame = game.HasWonGame();
    
            // Assert
            Assert.True(hasWonGame);
        }

        [Theory]
        [InlineData(new []{Colours.Red, Colours.Blue, Colours.Blue, Colours.Purple, Colours.Orange, Colours.Red, Colours.Yellow})]
        [InlineData(new []{Colours.Red, Colours.Blue, Colours.Blue, Colours.Green, Colours.Blue})]
        [InlineData(new []{Colours.Red, Colours.Blue, Colours.Blue})]
        [InlineData(new Colours[]{ })]
        public void
            GivenTheAnswerDoesntContainExactlyFourColours_WhenEvaluateAnswerIsCalled_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfColours(Colours[] invalidAnswer)
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var selectedColours = new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);

            game.Initialise();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => game.EvaluateAnswer(invalidAnswer));
            Assert.Equal(Constants.InvalidNumberOfColoursExceptionMessage, exception.Message); 
        }
        
        [Fact]
        public void GivenTheAnswerIsNotCorrect_WhenEvaluateAnswerIsCalledMoreThanTheMaxNumberOfGuesses_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfTries()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var selectedColours = new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green};
            var incorrectAnswer = new[] {Colours.Red, Colours.Orange, Colours.Blue, Colours.Yellow};
            
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
            game.Initialise();
    
            // Act
            for (var i = 0; i < Constants.MaxNumberOfGuesses; i++)
            {
                game.EvaluateAnswer(incorrectAnswer);
            }

            // Assert
            var exception = Assert.Throws<Exception>(() => game.EvaluateAnswer(incorrectAnswer));
            Assert.Equal(Constants.TooManyTriesExceptionMessage, exception.Message);
        }

    }
}