using Mastermind;
using Mastermind.Randomizer;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GameplayTests
    {
        [Fact] public void GivenTheGameIsInitialised_WhenItCreatesTheGoalColourArray_ThenShouldUseRandomizerToPickFourColours()
        {
            // Arrange
            var mockRandomizer = new Mock<IRandomizer>();
            var game = new Game(mockRandomizer.Object);
            var expectedSelectedColours = new[] {Colours.Red, Colours.Blue, Colours.Blue, Colours.Green};
            mockRandomizer.Setup(randomizer => randomizer.GetRandomFourColours())
                .Returns(expectedSelectedColours)
                .Verifiable();
            
            // Act
            game.Initialise();
            var actualSelectedColours = game.GetSelectedColours();
            
            // Assert
            mockRandomizer.Verify();
            Assert.Equal(expectedSelectedColours, actualSelectedColours);
        }
    }
}