using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Presentation.InputOutput;

namespace Mastermind
{
    public class Controller
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
        private bool _hasQuit;
        private Colour[] _userGuessedColours = Array.Empty<Colour>();
        public Controller(IInputOutput consoleInputOutput)
        {
            _inputOutput = consoleInputOutput;
        }
        
        public void DisplayInitialMessage()
        {
            _inputOutput.OutputWelcomeMessage();
        }
        
        public void UpdatePlayerGuess()
        {
            var playerInput = _inputOutput.GetAGuessInput();
            if (playerInput.HasQuit)
            {
                _hasQuit = true;
            }
            else
            {
                _userGuessedColours = ConvertStringToColours(playerInput.ColoursInput!);
            }
        }
        
        public Colour[] GetUpdatedUserGuess()
        {
            return _userGuessedColours;
        }

        public bool HasPLayerQuit()
        {
            return _hasQuit;
        }

        public void DisplayUpdatedGameInfo(Game game)
        {
            DisplayGuess(_userGuessedColours);
            DisplayClues(game.Clues);
            DisplayGuessesRemaining(game.GuessingCount);
        }

        private void DisplayGuessesRemaining(int gameGuessingCount)
        {
            _inputOutput.OutputGuessesRemaining(ValidConditions.MaxNumberOfGuesses - gameGuessingCount);
        }

        public void DisplayClues(List<Clue> clues)
        {
            var outputClues = clues.Select(t => DefaultClues[t]).ToList();

            _inputOutput.OutputClues(outputClues);
        }

        private void DisplayGuess(Colour[] userGuess)
        {
            var outputColours = ConvertColourToString(userGuess);
            _inputOutput.OutputColourArray(outputColours);
        }
        
        public void DisplayEndGameResult(GameStatus gameStatus, Game game)
        {
            switch (gameStatus)
            {
                case GameStatus.Won:
                    _inputOutput.OutputGameWonMessage();
                    break;
                case GameStatus.Quit:
                    _inputOutput.OutputGameQuitMessage();
                    break;
            }
            DisplayGuessesRemaining(game.GuessingCount);
            DisplayGuess(game.SelectedColours);
        }

        private string[] ConvertColourToString(Colour[] colours)
        {
            var colourString = new string[ValidConditions.SelectedNumberOfColours];

            for (var index = 0; index < colours.Length; index++)
            {
                colourString[index] = DefaultColours[colours[index]];
            }
            return colourString;
        }

        private Colour[] ConvertStringToColours(string[] colourString)
        {
            var colours = new Colour[ValidConditions.SelectedNumberOfColours];

            for (var i = 0; i < colourString.Length; i++)
            {
                var colour = colourString[i];
                var tempColour = DefaultColours.First(c => c.Value == colour).Key;
                colours[i] = tempColour;
            }
            return colours;
        }
    }
}