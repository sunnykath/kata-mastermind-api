namespace Mastermind.Domain.Models;

public record PlayerInput
{
    public IEnumerable<string>? ColoursInput { get; init; }
    public bool HasQuit { get; init; }
}