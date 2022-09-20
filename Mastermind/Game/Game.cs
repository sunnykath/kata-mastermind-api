using Mastermind.Enums;
using Mastermind.Randomizer;

namespace Mastermind.Game
{
    public class Game
    {
        private Colour[] _selectedColours;
        private readonly List<Clue> _clues;
        private bool _hasWonGame;
        private int _guessingCount;
        
        private readonly IRandomizer _randomizer;
        
        public Game(IRandomizer randomizer)
        {
            _randomizer = randomizer;
            _selectedColours = Array.Empty<Colour>();
            _clues = new List<Clue>();
            _hasWonGame = false;
            _guessingCount = 0;
        }

        public void Initialise()
        {
            _selectedColours = _randomizer.GetRandomColours(Constants.SelectedNumberOfColours);
            _guessingCount = 0;
        }
        
        public void EvaluatePredictedAnswer(Colour[] predictedAnswer)
        {
            GameValidator.ValidateNumberOfGuesses(_guessingCount);
            _guessingCount++;

            GameValidator.ValidateInputArray(predictedAnswer);
            
            UpdateCluesAccordingThePrediction(predictedAnswer);

            UpdateGameWonStatus();
        }
        
        public Colour[] GetSelectedColours()
        {
            return _selectedColours;
        }
        
        public IEnumerable<Clue> GetClues()
        {
            return _clues;
        }

        public bool HasWonGame()
        {
            return _hasWonGame;
        }

        private void UpdateCluesAccordingThePrediction(Colour[] predictedAnswer)
        {
            for (var index = 0; index < _selectedColours.Length; index++)
            {
                var selectedColour = _selectedColours[index];
                
                if (!predictedAnswer.Contains(selectedColour)) continue;

                _clues.Add(predictedAnswer[index] == selectedColour ? Clue.Black : Clue.White);
            }
        }
        
        private void UpdateGameWonStatus()
        {
            if (_clues.Count(c => c == Clue.Black) == 4)
            {
                _hasWonGame = true;
            }
        }
    }
}