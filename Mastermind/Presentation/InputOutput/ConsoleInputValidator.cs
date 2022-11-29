using Mastermind.Domain.BusinessRules;

namespace Mastermind.Presentation.InputOutput;

public class ConsoleInputValidator
{
    private const string QuitCommand = "q";
    private const char LowerLimit = '0';
    private const char UpperLimit = '5';
    public bool ValidateInput(string input)
    {
        if (input == QuitCommand) return true;
        
        if (input.Length != GameConstants.SelectedNumberOfColours) return false;
        
        foreach (var character in input)
        {
            if (!char.IsDigit(character)) return false;
            
            if (character is < LowerLimit or > UpperLimit) return false;
        }

        return true;
    }

}