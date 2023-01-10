using Mastermind.Application;
using Mastermind.Application.Randomizer;
using Mastermind.Domain;
using Mastermind.Presentation.InputOutput;

namespace Mastermind.Presentation;

public class Mastermind
{
    private readonly View _view;
    private readonly Controller _controller;
    
    public Mastermind(IInputOutput inputOutput, IRandomizer randomizer)
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