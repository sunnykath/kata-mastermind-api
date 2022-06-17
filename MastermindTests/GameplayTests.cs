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
        
        [Fact]
        public void GivenTheSelectedColourArrayHasBeenCreated_WhenEvaluateAnswerIsFollowedByTheGetCluesCall_ThenShouldReturnTheCorrectClues()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            mockRandomizer.Setup(randomizer => randomizer.GetRandomColours(Constants.NumberOfColoursToSelect))
                .Returns(new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green});
            
            game.Initialise();
            
            var predictedAnswer = new[] {Colours.Green, Colours.Blue, Colours.Orange, Colours.Red};
            const int expectedWhiteClueCount = 3;
            const int expectedBlackClueCount = 1;
            
            // Act
            game.EvaluateAnswer(predictedAnswer);
            var clues = game.GetClues();
            
            // Assert
            Assert.Equal(expectedWhiteClueCount, clues.Count(c => c == Clue.White));
            Assert.Equal(expectedBlackClueCount, clues.Count(c => c == Clue.Black));
        }
    }
}