using Mastermind.Domain;

namespace Mastermind.Presentation.InputOutput;

public interface IInput
{
    public PlayerInput GetAGuessInput();
}