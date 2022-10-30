using Mastermind.Domain.BusinessRules;
using Mastermind.Domain.Models;

namespace Mastermind.Presentation.InputOutput
{
    public class ConsoleInputOutput : IInputOutput
    {
        private static readonly string[] AllColours = { 
            ColourSquares.Red,
            ColourSquares.Blue,
            ColourSquares.Green,
            ColourSquares.Orange,
            ColourSquares.Purple,
            ColourSquares.Yellow
        };

        private const char CharToIntConversion = '0';

        public PlayerInput GetAGuessInput()
        {
            PrintGetUserGuessMessage();
            return GetTheUserGuessedColours();
        }

        public void OutputWelcomeMessage()
        {
            PrintOutput(ConsoleMessages.Title);
            PrintOutput(ConsoleMessages.GameInformation);
        }

        public void OutputColourArray(string[] colours)
        {
            PrintOutput("Here's your colours : " + string.Join(" ", colours));
        }

        public void OutputClues(List<string> clues)
        {
            PrintOutput("Here's your clues : " + string.Join(" ", clues) + " ");
        }

        public void OutputGameWonMessage()
        {
            PrintOutput(ConsoleMessages.GameWonMessage);
        }

        public void OutputGameQuitMessage()
        {
            PrintOutput(ConsoleMessages.GameQuitMessage);
        }

        public void OutputGuessesRemaining(int guessesRemaining)
        {
            PrintOutput(ConsoleMessages.GuessesRemainingMessage + guessesRemaining);
        }

        private void PrintGetUserGuessMessage()
        {
            Console.WriteLine(ConsoleMessages.GetUserGuessMessage);
            DisplayTheColoursWithTheirIndexes();
        }

        private PlayerInput GetTheUserGuessedColours()
        {
            Console.Write(ConsoleMessages.GetUserGuessPrompt);
            var userInput = GetValidInput();
            
            if (userInput == "q")
                return new PlayerInput
                {
                    HasQuit = true
                };

            var playerGuessedColours = new string[ValidConditions.SelectedNumberOfColours];
            for (var i = 0; i < ValidConditions.SelectedNumberOfColours; i++)
            {
                playerGuessedColours[i] = AllColours[userInput[i] - CharToIntConversion];
            }

            return new PlayerInput
            {
                ColoursInput = playerGuessedColours
            };
        }

        private static string GetValidInput()
        {
            var inputValidator = new ConsoleInputValidator();
            bool isInputValid;
            string input;
            do
            { 
                input = Console.ReadLine()!;
                
                isInputValid = inputValidator.ValidateInput(input);
                if (!isInputValid)
                {
                    Console.Write(ConsoleMessages.InvalidInputMessage);
                }
            } while (!isInputValid);

            return input;
        }

        private void DisplayTheColoursWithTheirIndexes()
        {
            const string numberOutputs = "\t0\t1\t2\t3\t4\t5\t\n";
            var colorOutputs = AllColours.Aggregate("", (output, color) => output + $"\t{color}");
            
            PrintOutput(numberOutputs + colorOutputs + "\n");
        }
        private static void PrintOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
