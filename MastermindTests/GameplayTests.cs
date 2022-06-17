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
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.NumberOfColoursToSelect))
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
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.NumberOfColoursToSelect))
                .Returns(new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green});
            
            game.Initialise();
            
            // Act
            game.EvaluateAnswer(predictedAnswer);
            var clues = game.GetClues();
            
            // Assert
            Assert.Equal(expectedWhiteClueCount, clues.Count(c => c == Clue.White));
            Assert.Equal(expectedBlackClueCount, clues.Count(c => c == Clue.Black));
        }
    }
}