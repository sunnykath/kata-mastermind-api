using System.Collections;
using Mastermind.Domain.Models;

namespace Mastermind.Randomizer
{
    public class DefaultRandomizer : IRandomizer
    {
        private readonly Random _random = new ();
        
        public Colour[] GetRandomColours(int numberOfColoursToSelect)
        {
            var selectedColours = new Colour[numberOfColoursToSelect];
            var seededColours = (IList) Enum.GetValues(typeof(Colour));
            
            for (var index = 0; index < numberOfColoursToSelect; index++)
            {
                var randomIndex = _random.Next(seededColours.Count);
                var randomColour = (Colour) seededColours[randomIndex]!;
                selectedColours[index] = randomColour;
            }
            return selectedColours;
        }

        public List<Clue> GetShuffledArray(List<Clue> orderedArray)
        {
            return orderedArray.OrderBy(_ => _random.Next()).ToList();
        }
    }
}