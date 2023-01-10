using Mastermind.Application.BusinessRules;
using Mastermind.Application.Randomizer;
using Mastermind.Domain;

namespace Mastermind.Application;

public class Controller
{
    private readonly GamePlay _gamePlay;
    
    public Controller(IRandomizer randomizer)
    {
        _gamePlay = new GamePlay(randomizer);
    }
    public Game StartNewGame()
    {
        return _gamePlay.SetupGame();
    }

    public Game UpdateGameWithLastPlayerGuess(Game updatedGame)
    {
        if (updatedGame.GameState != GameStatus.Quit)
        {
            _gamePlay.EvaluatePredictedAnswer(updatedGame);
        }
        return updatedGame;
    }
}