using Mastermind.Randomizer;

namespace Mastermind
{
    public class Game
    {
        private Colours[] _selectedColours;
        private readonly IRandomizer _randomizer;
        public Game(IRandomizer randomizer)
        {
            _randomizer = randomizer;
            _selectedColours = System.Array.Empty<Colours>();
        }

        public void Initialise()
        {
            _selectedColours = _randomizer.GetRandomColours(Constants.NumberOfColoursToSelect);
        }

        public Colours[] GetSelectedColours()
        {
            return _selectedColours;
        }
    }
}