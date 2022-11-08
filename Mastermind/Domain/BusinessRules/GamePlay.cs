using Mastermind.Domain.Models;
using Mastermind.Randomizer;

namespace Mastermind.Domain.BusinessRules
{
    public class GamePlay
    {
        private readonly IRandomizer _randomizer;
        public Game Game { get; set; } = new(){GameState = GameStatus.Playing};
        
        public GamePlay(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public void SetupGame()
        {
            Game.SelectedColours = _randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours);
            Game.GuessingCount = 0;
        }
        
        public void EvaluatePredictedAnswer()
        {
            GameValidator.ValidateNumberOfGuesses(Game.GuessingCount);
            Game.GuessingCount++;

            var prediction = Game.LatestPlayerGuess;
            GameValidator.ValidateInputArray(prediction);
            
            UpdateCluesAccordingThePrediction(prediction);
            ShuffleClues();

            UpdateGameWonStatus();
        }

        private void ShuffleClues()
        {
            Game.Clues = _randomizer.GetShuffledArray(Game.Clues);
        }

        private void UpdateCluesAccordingThePrediction(Colour[] predictedAnswer)
        {
            Game.Clues?.Clear();
            for (var index = 0; index < Game.SelectedColours.Length; index++)
            {
                var selectedColour = Game.SelectedColours[index];
                
                if (!predictedAnswer.Contains(selectedColour)) continue;

                Game.Clues?.Add(predictedAnswer[index] == selectedColour ? Clue.Black : Clue.White);
            }
        }
        
        private void UpdateGameWonStatus()
        {
            if (Game.Clues?.Count(c => c == Clue.Black) == 4)
            {
                Game.GameState = GameStatus.Won;
            }
        }
    }
}