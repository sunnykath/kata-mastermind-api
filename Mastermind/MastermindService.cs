using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;

namespace Mastermind;

public class MastermindService
{
    private readonly View _view;
    private readonly Controller _controller;
    
    public MastermindService(IInputOutput inputOutput, IRandomizer randomizer)
    {
        _view = new View(inputOutput);
        _controller = new Controller(randomizer);
    }

    public void PlayGame()
    {
        _view.DisplayInitialMessage();
        var game = _controller.StartNewGame();
        
        while (game.GameState == GameStatus.Playing)
        {
            _view.UpdateLastPlayerGuessInGame(game);
            
            if (game.GameState == GameStatus.Quit) continue;
            
            game = _controller.UpdateGameWithLastPlayerGuess(game);

            if (game.GameState == GameStatus.Won) continue;
            
            _view.DisplayUpdatedGameInfo(game);
        }

        _controller.EndGame();
        _view.DisplayEndGameResult(game);
    }
}