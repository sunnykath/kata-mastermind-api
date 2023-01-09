using Mastermind.Application.BusinessRules;
using Mastermind.Domain;
using Mastermind.Presentation.InputOutput;

namespace Mastermind.Presentation;

public class View
{
    private static readonly Dictionary<Colour, string> DefaultColours = new ()
    {
        {Colour.Red, ColourSquares.Red},
        {Colour.Blue, ColourSquares.Blue},
        {Colour.Green, ColourSquares.Green},
        {Colour.Orange, ColourSquares.Orange},
        {Colour.Purple, ColourSquares.Purple},
        {Colour.Yellow, ColourSquares.Yellow},
    };
    private static readonly Dictionary<Clue, string> DefaultClues = new()
    {
        {Clue.Black, ColourSquares.Black},
        {Clue.White, ColourSquares.White}
    };
    private readonly IInputOutput _inputOutput;
    public View(IInputOutput consoleInputOutput)
    {
        _inputOutput = consoleInputOutput;
    }
        
    public void DisplayInitialMessage()
    {
        _inputOutput.OutputWelcomeMessage();
    }
    public void UpdateLastPlayerGuessInGame(Game game)
    {
        var playerInput = _inputOutput.GetAGuessInput();
        if (playerInput.HasQuit)
        {
            game.GameState = GameStatus.Quit;
        }
        else
        {
            game.LatestPlayerGuess = ConvertStringToColours((string[])playerInput.ColoursInput!);
        }
    }
    public void DisplayGameInfo(Game game)
    {
        switch (game.GameState)
        {
            case GameStatus.Won:
                _inputOutput.OutputGameWonMessage();
                break;
            case GameStatus.Quit:
                _inputOutput.OutputGameQuitMessage();
                break;
            case GameStatus.Playing:
                DisplayClues(game.Clues!);
                break;
            case GameStatus.Lost:
                _inputOutput.OutputGameLostMessage();
                break;
        }
        DisplayGuess((Colour[])game.LatestPlayerGuess);
        DisplayGuessesRemaining(game.GuessingCount);
    }
        
    private void DisplayGuessesRemaining(int gameGuessingCount)
    {
        _inputOutput.OutputGuessesRemaining(GameConstants.MaxNumberOfGuesses - gameGuessingCount);
    }
    private void DisplayClues(IEnumerable<Clue> clues)
    {
        var outputClues = clues.Select(t => DefaultClues[t]).ToList();

        _inputOutput.OutputClues(outputClues);
    }
    private void DisplayGuess(Colour[] userGuess)
    {
        var outputColours = ConvertColourToString(userGuess);
        _inputOutput.OutputColours(outputColours);
    }
    private string[] ConvertColourToString(Colour[] colours)
    {
        var colourString = new string[GameConstants.SelectedNumberOfColours];

        for (var index = 0; index < colours.Length; index++)
        {
            colourString[index] = DefaultColours[colours[index]];
        }
        return colourString;
    }
    private Colour[] ConvertStringToColours(string[] colourString)
    {
        var colours = new Colour[GameConstants.SelectedNumberOfColours];

        for (var i = 0; i < colourString.Length; i++)
        {
            var colour = colourString[i];
            var tempColour = DefaultColours.First(c => c.Value == colour).Key;
            colours[i] = tempColour;
        }
        return colours;
    }
}