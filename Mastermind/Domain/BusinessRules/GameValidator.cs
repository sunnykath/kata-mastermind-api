namespace Mastermind.Domain.BusinessRules
{
    public static class GameValidator
    {
        public static void ValidateNumberOfGuesses(int guessingCount)
        {
            if (guessingCount == ValidConditions.MaxNumberOfGuesses)
            {
                throw new Exception(ExceptionMessages.TooManyTries);
            }
        }
        
        public static void ValidateInputArray<T>(IReadOnlyCollection<T> inputArray)
        {
            if (inputArray.Count != ValidConditions.SelectedNumberOfColours)
            {
                throw new Exception(ExceptionMessages.InvalidNumberOfColours);
            }
        }
    }
}