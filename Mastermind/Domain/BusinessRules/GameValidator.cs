namespace Mastermind.Domain.BusinessRules
{
    public static class GameValidator
    {
        public static void ValidateNumberOfGuesses(int guessingCount)
        {
            if (guessingCount == GameConstants.MaxNumberOfGuesses)
            {
                throw new Exception($"You have tried more than {GameConstants.MaxNumberOfGuesses} tries!");
            }
        }
        
        public static void ValidateInputArray<T>(IReadOnlyCollection<T> inputArray)
        {
            if (inputArray.Count != GameConstants.SelectedNumberOfColours)
            {
                throw new Exception("Answer array should only contain 4 colours.");
            }
        }
    }
}