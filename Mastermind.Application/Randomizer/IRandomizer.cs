using Mastermind.Domain;

namespace Mastermind.Application.Randomizer;

public interface IRandomizer
{
    public Colour[] GetRandomColours(int numberOfColoursToSelect);

    IEnumerable<Clue> GetShuffledArray(IEnumerable<Clue> orderedArray);
}