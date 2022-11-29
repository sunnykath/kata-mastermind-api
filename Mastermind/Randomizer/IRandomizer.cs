using Mastermind.Domain.Models;

namespace Mastermind.Randomizer
{
    public interface IRandomizer
    {
        public Colour[] GetRandomColours(int numberOfColoursToSelect);

        IEnumerable<Clue> GetShuffledArray(IEnumerable<Clue> orderedArray);
    }
}