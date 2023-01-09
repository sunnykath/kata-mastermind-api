namespace Mastermind.Domain;

public record PlayerInput
{
    public IEnumerable<string>? ColoursInput { get; init; }
    public bool HasQuit { get; init; }
}