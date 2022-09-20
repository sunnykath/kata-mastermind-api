using Mastermind.Enums;
using Mastermind.Presentation.InputOutput;

namespace Mastermind.Presentation
{
    public class View
    {
        private static readonly Dictionary<Colour, string> DefaultColours = new ()
        {
            {Colour.Red, Constants.RedSquare},
            {Colour.Blue, Constants.BlueSquare},
            {Colour.Green, Constants.GreenSquare},
            {Colour.Orange, Constants.OrangeSquare},
            {Colour.Purple, Constants.PurpleSquare},
            {Colour.Yellow, Constants.YellowSquare},
        };

        private static readonly Dictionary<Clue, string> DefaultClues = new()
        {
            {Clue.Black, Constants.BlackSquare},
            {Clue.White, Constants.WhiteSquare}
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
        
        public Colour[] GetUserGuess()
        {
            var playerInputtedColours = _inputOutput.GetAGuessInput();
           
            var userGuessedColours = ConvertStringToColours(playerInputtedColours);
            
            return userGuessedColours;
        }

        public void DisplayClues(List<Clue> clues)
        {
            // @TODO: magic number
            var outputClues = new List<string>();
            
            for (var i = 0; i < clues.Count; i++)
            {
                outputClues.Add(DefaultClues[clues[i]]);
            }
            _inputOutput.OutputClues(outputClues);
        }

        public void DisplayEndGameResult(GameStatus gameStatus, Colour[] correctAnswer)
        {
            switch (gameStatus)
            {
                case GameStatus.Won:
                    _inputOutput.OutputGameWonMessage();
                    break;
                case GameStatus.Quit:
                    _inputOutput.OutputGameQuitMessage();
                    break;
                // @DO I NEED THIS??
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _inputOutput.OutputColourArray(ConvertColourToString(correctAnswer));
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