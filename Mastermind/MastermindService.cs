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
            
            game = _controller.UpdateGameWithLastPlayerGuess(game);

            _view.DisplayGameInfo(game);
        }
    }
}