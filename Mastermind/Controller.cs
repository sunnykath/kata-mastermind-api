using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Randomizer;

namespace Mastermind;

public class Controller
{
    private readonly GamePlay _gamePlay;
    
    public Controller(IRandomizer randomizer)
    {
        _gamePlay = new GamePlay(randomizer);
    }
    public Game StartNewGame()
    {
        _gamePlay.SetupGame();

        return _gamePlay.Game;
    }

    public Game UpdateGameWithLastPlayerGuess(Game updatedGame)
    {
        _gamePlay.Game = updatedGame;
        _gamePlay.EvaluatePredictedAnswer();
        return _gamePlay.Game;
    }

    public void EndGame()
    {
        _gamePlay.DeleteGame();
    }
}