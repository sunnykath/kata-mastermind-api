using Mastermind.Presentation;

namespace Mastermind.GamePlay
{
    public static class GameValidator
    {
        public static void ValidateNumberOfGuesses(int guessingCount)
        {
            if (guessingCount == Constants.MaxNumberOfGuesses)
            {
                throw new Exception(ConsoleMessages.TooManyTriesExceptionMessage);
            }
        }
        
        public static void ValidateInputArray<T>(IReadOnlyCollection<T> inputArray)
        {
            if (inputArray.Count != Constants.SelectedNumberOfColours)
            {
                throw new Exception(ConsoleMessages.InvalidNumberOfColoursExceptionMessage);
            }
        }
    }
}