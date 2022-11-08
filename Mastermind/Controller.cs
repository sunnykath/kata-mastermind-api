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
        return _gamePlay.SetupGame();
    }

    public Game UpdateGameWithLastPlayerGuess(Game updatedGame)
    {
        _gamePlay.EvaluatePredictedAnswer(updatedGame);
        return updatedGame;
    }
}