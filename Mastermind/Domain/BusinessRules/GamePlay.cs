using Mastermind.Domain.Models;
using Mastermind.Randomizer;

namespace Mastermind.Domain.BusinessRules
{
    public class GamePlay
    {
        private readonly IRandomizer _randomizer;
        
        public GamePlay(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public Game SetupGame()
        {
            return new Game
            {
                SelectedColours =_randomizer.GetRandomColours(ValidConditions.SelectedNumberOfColours),
                GuessingCount = 0
            };
        }
        
        public void EvaluatePredictedAnswer(Game game)
        {
            if (game.GameState == GameStatus.Quit) return;
            GameValidator.ValidateNumberOfGuesses(game.GuessingCount);
            game.GuessingCount++;

            GameValidator.ValidateInputArray(game.LatestPlayerGuess);
            
            UpdateCluesAccordingThePrediction(game);
            game.Clues = GetShuffledClues(game.Clues);

            UpdateGameWonStatus(game);
        }

        private List<Clue> GetShuffledClues(List<Clue> clues)
        {
            return _randomizer.GetShuffledArray(clues);
        }

        private void UpdateCluesAccordingThePrediction(Game game)
        {
            game.Clues?.Clear();
            var predictedAnswer = game.LatestPlayerGuess;
            
            for (var index = 0; index < game.SelectedColours.Length; index++)
            {
                var selectedColour = game.SelectedColours[index];
                
                if (!predictedAnswer.Contains(selectedColour)) continue;

                game.Clues?.Add(predictedAnswer[index] == selectedColour ? Clue.Black : Clue.White);
            }
        }
        
        private void UpdateGameWonStatus(Game game)
        {
            if (game.Clues?.Count(c => c == Clue.Black) == 4)
            {
                game.GameState = GameStatus.Won;
            }
        }
    }
}