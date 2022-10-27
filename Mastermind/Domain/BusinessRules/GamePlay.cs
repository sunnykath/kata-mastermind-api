using Mastermind.Domain.Models;
using Mastermind.Randomizer;

namespace Mastermind.Domain.BusinessRules
{
    public class GamePlay
    {
        private readonly IRandomizer _randomizer;
        private readonly Game _game;
        
        public GamePlay(IRandomizer randomizer, Game game)
        {
            _randomizer = randomizer;
            _game = game;
        }

        public void SetupGame()
        {
            _game.SelectedColours = _randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours);
            _game.GuessingCount = 0;
        }
        
        public void EvaluatePredictedAnswer(Colour[] predictedAnswer)
        {
            GameValidator.ValidateNumberOfGuesses(_game.GuessingCount);
            _game.GuessingCount++;

            GameValidator.ValidateInputArray(predictedAnswer);
            
            UpdateCluesAccordingThePrediction(predictedAnswer);
            ShuffleClues();

            UpdateGameWonStatus();
        }

        private void ShuffleClues()
        {
            _game.Clues = _randomizer.GetShuffledArray(_game.Clues);
        }

        private void UpdateCluesAccordingThePrediction(Colour[] predictedAnswer)
        {
            _game.Clues?.Clear();
            for (var index = 0; index < _game.SelectedColours.Length; index++)
            {
                var selectedColour = _game.SelectedColours[index];
                
                if (!predictedAnswer.Contains(selectedColour)) continue;

                _game.Clues?.Add(predictedAnswer[index] == selectedColour ? Clue.Black : Clue.White);
            }
        }
        
        private void UpdateGameWonStatus()
        {
            if (_game.Clues?.Count(c => c == Clue.Black) == 4)
            {
                _game.HasWonGame = true;
            }
        }
    }
}