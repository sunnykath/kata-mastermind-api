using Mastermind.Domain.Models;

namespace Mastermind.Presentation.InputOutput;

public interface IInput
{
    public PlayerInput GetAGuessInput();
}