using System.Collections.Generic;

namespace Mastermind.Randomizer
{
    public interface IRandomizer
    {
        public Colour[] GetRandomColours(int numberOfColoursToSelect);
        
    }
}