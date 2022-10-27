using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Randomizer;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GameplayTests
    {
        private readonly Mock<IRandomizer> _mockRandomizer;
        private readonly Game _game;
        private readonly GamePlay _gamePlay;

        public GameplayTests()
        {
            _mockRandomizer = new Mock<IRandomizer>();
            _game = new Game();
            _gamePlay = new GamePlay(_mockRandomizer.Object, _game);
        }
        
        [Fact] 
        public void GivenTheGameIsInitialised_WhenItCreatesTheSelectedColours_ThenShouldRandomlyPickFourColours()
        {
            // Arrange
            var expectedSelectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(expectedSelectedColours)
                .Verifiable();
            
            // Act
            _gamePlay.SetupGame();
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
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green});
            _mockRandomizer.Setup(randomizer =>
                    randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
                .Returns<List<Clue>>(clues => clues);
            
            _gamePlay.SetupGame();
            
            // Act
            _gamePlay.EvaluatePredictedAnswer(predictedAnswer);
            var clues = _game.Clues;
            
            // Assert
            Assert.Equal(expectedWhiteClueCount, clues.Count(c => c == Clue.White));
            Assert.Equal(expectedBlackClueCount, clues.Count(c => c == Clue.Black));
        }

        [Fact]
        public void GivenTheSelectedColourArrayHasBeenCreated_WhenEvaluateAnswerIsFollowedByTheGetCluesCall_ThenShouldReturnTheCorrectCluesShuffledByTheRandomizer()
        {
            // Arrange
            var predictedAnswer = new[] { Colour.Red, Colour.Blue, Colour.Yellow, Colour.Purple };
            var expectedClues = new List<Clue> { Clue.Black, Clue.White, Clue.Black };
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green});
            _mockRandomizer.Setup(randomizer =>
                    randomizer.GetShuffledArray(new List<Clue> { Clue.Black, Clue.Black, Clue.White }))
                .Returns(expectedClues)
                .Verifiable();
            
            _gamePlay.SetupGame();
            
            // Act
            _gamePlay.EvaluatePredictedAnswer(predictedAnswer);
            var actualClues = _game.Clues;

            // Assert
            Assert.Equal(expectedClues, actualClues);
            _mockRandomizer.Verify();
        }

        [Fact]
        public void GivenTheAnswerIsTheSameAsTheSelectedArray_WhenEvaluateAnswerIsFollowedByTheHasWonGameCall_ThenShouldReturnTrue()
        {
            // Arrange
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(selectedColours);
            _mockRandomizer.Setup(randomizer =>
                    randomizer.GetShuffledArray(It.IsAny<List<Clue>>()))
                .Returns<List<Clue>>(clues => clues);

            _gamePlay.SetupGame();

            // Act
            _gamePlay.EvaluatePredictedAnswer(selectedColours);
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
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(selectedColours);
        
            _gamePlay.SetupGame();
        
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => (_gamePlay).EvaluatePredictedAnswer(invalidAnswer));
            Assert.Equal(ExceptionMessages.InvalidNumberOfColours, exception.Message); 
        }
        
        [Fact]
        public void GivenTheAnswerIsNotCorrect_WhenEvaluateAnswerIsCalledMoreThanTheMaxNumberOfGuesses_ThenShouldThrowExceptionWithAnInvalidMessageForTheNumberOfTries()
        {
            // Arrange
            var selectedColours = new[] {Colour.Red, Colour.Blue, Colour.Blue, Colour.Green};
            var incorrectAnswer = new[] {Colour.Red, Colour.Orange, Colour.Blue, Colour.Yellow};
            
            _mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours))
                .Returns(selectedColours);
            _mockRandomizer.Setup(randomizer =>
                    randomizer.GetShuffledArray(new List<Clue> { Clue.Black, Clue.Black, Clue.White }))
                .Returns(new List<Clue>());
            _gamePlay.SetupGame();
        
            // Act
            for (var i = 0; i < ValidConditions.MaxNumberOfGuesses; i++)
            {
                _gamePlay.EvaluatePredictedAnswer(incorrectAnswer);
            }
        
            // Assert
            var exception = Assert.Throws<Exception>(() => _gamePlay.EvaluatePredictedAnswer(incorrectAnswer));
            Assert.Equal(ExceptionMessages.TooManyTries, exception.Message);
        }
    }
}