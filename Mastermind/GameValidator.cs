using System;
using System.Collections.Generic;

namespace Mastermind
{
    public static class GameValidator
    {
        public static void ValidateNumberOfGuesses(int guessingCount)
        {
            if (guessingCount == Constants.MaxNumberOfGuesses)
            {
                throw new Exception(Constants.TooManyTriesExceptionMessage);
            }
        }
        
        public static void ValidateInputArray<T>(IReadOnlyCollection<T> inputArray)
        {
            if (inputArray.Count != Constants.SelectedNumberOfColours)
            {
                throw new Exception(Constants.InvalidNumberOfColoursExceptionMessage);
            }
        }
    }
}