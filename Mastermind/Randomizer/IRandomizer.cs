using System.Collections.Generic;
using Mastermind.Enums;

namespace Mastermind.Randomizer
{
    public interface IRandomizer
    {
        public Colour[] GetRandomColours(int numberOfColoursToSelect);

        List<Clue> GetShuffledArray(List<Clue> orderedArray);
    }
}