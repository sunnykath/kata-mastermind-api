using Mastermind.Enums;
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

    public void PlayGame(IRandomizer randomizer)
    {
        _view.DisplayInitialMessage();
        
        var gameChecker = new GameChecker(randomizer, _game);
        gameChecker.Initialise();
        var gameStatus = GameStatus.Playing;

        while (gameStatus == GameStatus.Playing)
        {
            _view.UpdatePlayerGuess();

            if (_view.HasPLayerQuit())
            {
                gameStatus = GameStatus.Quit;
            }
            else
            {
                var userGuess = _view.GetUpdatedUserGuess();
                _view.DisplayGuess(userGuess);
                gameChecker.EvaluatePredictedAnswer(userGuess);

                if (_game.HasWonGame)
                {
                    gameStatus = GameStatus.Won;
                }
                else
                {
                    _view.DisplayClues(_game.Clues);
                }
            }
        }
        _view.DisplayEndGameResult(gameStatus, _game.SelectedColours);
    }
}