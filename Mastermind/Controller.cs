using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;
using Mastermind.Presentation.InputOutput;

namespace Mastermind
{
    public class Controller
    {
        private static readonly Dictionary<Colour, string> DefaultColours = new ()
        {
            {Colour.Red, Squares.Red},
            {Colour.Blue, Squares.Blue},
            {Colour.Green, Squares.Green},
            {Colour.Orange, Squares.Orange},
            {Colour.Purple, Squares.Purple},
            {Colour.Yellow, Squares.Yellow},
        };

        private static readonly Dictionary<Clue, string> DefaultClues = new()
        {
            {Clue.Black, Squares.Black},
            {Clue.White, Squares.White}
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
            // @TODO: magic number
            var colourString = new string[4];

            for (var index = 0; index < colours.Length; index++)
            {
                colourString[index] = DefaultColours[colours[index]];
            }
            return colourString;
        }

        private Colour[] ConvertStringToColours(string[] colourString)
        {
            // @TODO: magic number
            var colours = new Colour[4];

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