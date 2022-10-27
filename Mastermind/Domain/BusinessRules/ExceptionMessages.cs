namespace Mastermind.Domain.BusinessRules;

public static class ExceptionMessages
{
    public const string InvalidNumberOfColours = "Answer array should only contain 4 colours.";
    public static readonly string TooManyTries = $"You have tried more than {ValidConditions.MaxNumberOfGuesses} tries!";
}