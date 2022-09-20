namespace Mastermind.Presentation;

public static class ConsoleMessages
{
    
    public const string InvalidNumberOfColoursExceptionMessage = "Answer array should only contain 4 colours.";
    public static readonly string TooManyTriesExceptionMessage = $"You have tried more than {Constants.MaxNumberOfGuesses} tries!";

    public const string GetUserGuessMessage = "You can pick your four colours from the following!";
    public const string GetUserGuessPrompt = "Please enter the numbers to select the respective colours (e.g. 2345) : ";
    public const string InvalidInputMessage = "Invalid Input, Please enter valid numbers between 0-5: ";

    public const string GameWonMessage = "Congratulations! You have won the game by guessing the correct colours as shown below: \n";
    public const string GameQuitMessage = "You have quit the game, here's the answer: \n";


    public static readonly string GameInformation = $"Guess the combination of randomly selected {Constants.SelectedNumberOfColours} Colours! You have {Constants.MaxNumberOfGuesses} tries. Good Luck 👍 \n";

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