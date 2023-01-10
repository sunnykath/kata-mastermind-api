using Mastermind.Application.Randomizer;
using Mastermind.Domain;

namespace Mastermind.Application.BusinessRules;

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
            SelectedColours =_randomizer.GetRandomColours(GameConstants.SelectedNumberOfColours),
            GuessingCount = 0,
            GameState = GameStatus.Playing
        };
    }
        
    public void EvaluatePredictedAnswer(Game game)
    {
        UpdateGameLostStatus(game);

        game.IncrementGuesses();
            
        UpdateCluesAccordingThePrediction(game);
        game.Clues = GetShuffledClues(game.Clues!);

        UpdateGameWonStatus(game);
    }

    private IEnumerable<Clue> GetShuffledClues(IEnumerable<Clue> clues)
    {
        return _randomizer.GetShuffledArray(clues);
    }

    private void UpdateCluesAccordingThePrediction(Game game)
    {
        var playerGuess = game.LatestPlayerGuess.ToArray();
        var selectedColours = game.SelectedColours.ToArray();
        var clues = new List<Clue>();
            
        for (var index = 0; index < selectedColours.Length; index++)
        {
            var selectedColour = selectedColours[index];
                
            if (!playerGuess.Contains(selectedColour)) continue;
            clues.Add(playerGuess[index] == selectedColour ? Clue.Black : Clue.White);
        }
        game.Clues = clues;
    }
        
    private void UpdateGameWonStatus(Game game)
    {
        if (game.Clues?.Count(c => c == Clue.Black) == GameConstants.SelectedNumberOfColours)
        {
            game.GameState = GameStatus.Won;
        }
    }
    private void UpdateGameLostStatus(Game game)
    {
        if (game.GuessingCount == GameConstants.MaxNumberOfGuesses)
        {
            game.GameState = GameStatus.Lost;
        }
    }
}