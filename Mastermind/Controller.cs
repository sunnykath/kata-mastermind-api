using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;
using Mastermind.GamePlay;

namespace Mastermind;

public class Controller
{
    private readonly View _view;
    private readonly Game _game;
    
    public Controller(IInputOutput inputOutput)
    {
        _game = new Game();
        _view = new View(inputOutput);
    }

    public void Initialise()
    {
        _view.DisplayInitialMessage();
    }

    public void PlayGame(IRandomizer randomizer)
    {
        var gameChecker = new GameChecker(randomizer, _game);

        var userGuess = _view.GetUserGuess();
        _view.DisplayGuess(userGuess);
        gameChecker.EvaluatePredictedAnswer(userGuess);
        
        _view.DisplayClues(_game.Clues);
    }
}