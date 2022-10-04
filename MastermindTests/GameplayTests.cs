using System;
using System.Linq;
using Mastermind;
using Mastermind.Enums;
using Mastermind.GamePlay;
using Mastermind.Presentation;
using Mastermind.Randomizer;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GameplayTests
    {
        private readonly Mock<IRandomizer> _mockRandomizer;
        private readonly Game _game;
        private readonly GameChecker _gameChecker;

        public GameplayTests()
        {
            _mockRandomizer = new Mock<IRandomizer>();
            _game = new Game();
            _gameChecker = new GameChecker(_mockRandomizer.Object, _game);
        }
        
        [Fact] 
        public void GivenTheGameIsInitialised_WhenItCreatesTheSelectedColours_ThenShouldRandomlyPickFourColours()
        {
            // Arrange
            var expectedSelectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(expectedSelectedColours)
                .Verifiable();
            
            // Act
            _gameChecker.Initialise();
            var actualSelectedColours = _game.SelectedColours;
            
            // Assert
            _mockRandomizer.Verify();
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
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green});
            
            _gameChecker.Initialise();
            
            // Act
            _gameChecker.EvaluatePredictedAnswer(predictedAnswer);
            var clues = _game.Clues;
            
            // Assert
            Assert.Equal(expectedWhiteClueCount, clues.Count(c => c == Clue.White));
            Assert.Equal(expectedBlackClueCount, clues.Count(c => c == Clue.Black));
        }
        
        [Fact]
        public void GivenTheAnswerIsTheSameAsTheSelectedArray_WhenEvaluateAnswerIsFollowedByTheHasWonGameCall_ThenShouldReturnTrue()
        {
            // Arrange
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);

            _gameChecker.Initialise();

            // Act
            _gameChecker.EvaluatePredictedAnswer(selectedColours);
            var hasWonGame = _game.HasWonGame;
        
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
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
        
            _gameChecker.Initialise();
        
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => (_gameChecker).EvaluatePredictedAnswer(invalidAnswer));
            Assert.Equal(ConsoleMessages.InvalidNumberOfColoursExceptionMessage, exception.Message); 
        }
        
        [Fact]
        public void GivenTheAnswerIsNotCorrect_WhenEvaluateAnswerIsCalledMoreThanTheMaxNumberOfGuesses_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfTries()
        {
            // Arrange
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            var incorrectAnswer = new[] {Colour.Red, Colour.Orange, Colour.Blue, Colour.Yellow};
            
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.SelectedNumberOfColours))
                .Returns(selectedColours);
            _gameChecker.Initialise();
        
            // Act
            for (var i = 0; i < Constants.MaxNumberOfGuesses; i++)
            {
                _gameChecker.EvaluatePredictedAnswer(incorrectAnswer);
            }
        
            // Assert
            var exception = Assert.Throws<Exception>(() => _gameChecker.EvaluatePredictedAnswer(incorrectAnswer));
            Assert.Equal(ConsoleMessages.TooManyTriesExceptionMessage, exception.Message);
        }
    }
}