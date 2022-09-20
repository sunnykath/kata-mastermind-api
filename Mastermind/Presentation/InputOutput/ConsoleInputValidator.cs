namespace Mastermind.Presentation.InputOutput;

public class ConsoleInputValidator
{
    private readonly List<int> _validatedInput = new(4);

    public int[] GetValidatedInput()
    {
        return _validatedInput.ToArray();
    }

    public bool ValidateInput(string input)
    {
        if (input.Length != 4) return false;
        
        foreach (var character in input)
        {
            var isInteger = int.TryParse(character.ToString(), out var digit);
            
            if (!isInteger) return false;
            
            if (digit is < 0 or > 5) return false;
            
            _validatedInput.Add(digit);
        }

        return true;
    }

}