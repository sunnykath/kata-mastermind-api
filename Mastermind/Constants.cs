using System.Collections.Generic;

namespace Mastermind
{
    public static class Constants
    {
        public const string RedSquare   = "🟥";
        public const string BlueSquare  = "🟦";
        public const string GreenSquare = "🟩";
        public const string OrangeSquare = "🟧";
        public const string PurpleSquare = "🟪";
        public const string YellowSquare = "🟨";
        
        public const string BlackSquare = "⬛";
        public const string WhiteSquare = "⬜";

        public const string EmptySquare = "🔳";

        public const int SelectedNumberOfColours = 4;
        public const int MaxNumberOfGuesses = 60;

        public const string InvalidNumberOfColoursExceptionMessage = "Answer array should only contain 4 colours.";
        public static readonly string TooManyTriesExceptionMessage = $"You have tried more than {MaxNumberOfGuesses} tries!";

        public const string GetInputPrompt = "Please use the colours to prepare your guess and press 'c' to check against the answer\n";
        public const string DefaultColourRow = $"{RedSquare} {BlueSquare} {GreenSquare} {OrangeSquare} {PurpleSquare} {YellowSquare}\n";

        public const string GameWonMessage = "Congratulations! You have won the game by guessing the correct colours as shown below: \n";
        public const string GameQuitMessage = "You have quit the game, here's the answer: \n";


        public static readonly string GameInformation = $"Guess the combination of randomly selected {SelectedNumberOfColours} Colours! You have {MaxNumberOfGuesses} tries. Good Luck 👍 \n";

        public const string Title = @"
            Welcome To 
            
            ███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗██████╗ 
            ████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗
            ██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║██║  ██║
            ██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██║  ██║
            ██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██████╔╝
            ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═════╝ 

            ";
    }
}