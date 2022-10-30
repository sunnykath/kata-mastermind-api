using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;

namespace Mastermind;

public class MastermindService
{
    private readonly Controller _controller;
    private readonly Game _game;
    
    public MastermindService(IInputOutput inputOutput)
    {
        _game = new Game();
        _controller = new Controller(inputOutput);
    }

    public void PlayGame(IRandomizer randomizer)
    {
        _controller.DisplayInitialMessage();
        
        var gamePlay = new GamePlay(randomizer, _game);
        gamePlay.SetupGame();
        var gameStatus = GameStatus.Playing;

        while (gameStatus == GameStatus.Playing)
        {
            _controller.UpdatePlayerGuess();

            if (_controller.HasPLayerQuit())
            {
                gameStatus = GameStatus.Quit;
            }
            else
            {
                var userGuess = _controller.GetUpdatedUserGuess();
                gamePlay.EvaluatePredictedAnswer(userGuess);

                if (_game.HasWonGame)
                {
                    gameStatus = GameStatus.Won;
                }
                else
                {
                    _controller.DisplayUpdatedGameInfo(_game);
                }
            }
        }
        _controller.DisplayEndGameResult(gameStatus, _game);
    }
}