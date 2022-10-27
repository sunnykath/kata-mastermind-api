namespace Mastermind.Domain.Models;

public record PlayerInput
{
    public string[]? ColoursInput { get; init; }
    public bool HasQuit { get; init; }
}