namespace Mastermind.Presentation.InputOutput;

public class ConsoleInputValidator
{
    public bool ValidateInput(string input)
    {
        if (input == "q") return true;
        
        if (input.Length != 4) return false;
        
        foreach (var character in input)
        {
            if (!char.IsDigit(character)) return false;
            
            if (character is < '0' or > '5') return false;
        }

        return true;
    }

}