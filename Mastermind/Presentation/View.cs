using System.Collections.Generic;
using System.Linq;
using Mastermind.Presentation.InputOutput;

namespace Mastermind.Presentation
{
    public class View
    {
        public static readonly Dictionary<Colour, string> DefaultColours = new ()
        {
            {Colour.Red, Constants.RedSquare},
            {Colour.Blue, Constants.BlueSquare},
            {Colour.Green, Constants.GreenSquare},
            {Colour.Orange, Constants.OrangeSquare},
            {Colour.Purple, Constants.PurpleSquare},
            {Colour.Yellow, Constants.YellowSquare},
        };

        public static readonly Dictionary<Clue, string> DefaultClues = new()
        {
            {Clue.Black, Constants.BlackSquare},
            {Clue.White, Constants.WhiteSquare}
        };

        private readonly IInputOutput _inputOutput;
        public View(IInputOutput consoleInputOutput)
        {
            _inputOutput = consoleInputOutput;
        }

        public void DisplayGameDetails()
        {
            _inputOutput.DisplayOutput(Constants.Title);
            _inputOutput.DisplayOutput(Constants.GameInformation);
        }

        public Colour[] GetUserGuess()
        {
            var playerInputtedColours = _inputOutput.GetPlayerInput();
            var userGuessedColours = new Colour[4];

            for (var i = 0; i < playerInputtedColours.Length; i++)
            {
                var colour = playerInputtedColours[i];
                var tempColour = DefaultColours.First(c => c.Value == colour).Key;
                userGuessedColours[i] = tempColour;
            }

            return userGuessedColours;
        }

        public void DisplayClues(Clue[] clues)
        {
            var outputClues = clues.Aggregate("", (current, clue) => current + DefaultClues[clue] + " ");
            
            for (var i = 0; i < 4 - clues.Length; i++)
            {
                outputClues += Constants.EmptySquare + " ";
            }
            _inputOutput.DisplayOutput(outputClues + "\n");
        }
    }
}