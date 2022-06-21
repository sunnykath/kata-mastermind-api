namespace Mastermind
{
    public static class Constants
    {
        public const int SelectedNumberOfColours = 4;
        public const int MaxNumberOfGuesses = 60;

        public const string InvalidNumberOfColoursExceptionMessage = "Answer array should only contain 4 colours.";
        public static readonly string TooManyTriesExceptionMessage = $"You have tried more than {MaxNumberOfGuesses} tries!";
    }
}