using Mastermind.Domain;

namespace Mastermind.Console.InputOutput;

public interface IInput
{
    public PlayerInput GetAGuessInput();
}