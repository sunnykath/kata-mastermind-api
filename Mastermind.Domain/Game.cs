namespace Mastermind.Domain;

public class Game
{
    public Guid Id { get; set; }
    public GameStatus GameState { get; set; }
    public IEnumerable<Clue>? Clues { get; set; }
    public int GuessingCount { get; set; }
    public IEnumerable<Colour> LatestPlayerGuess { get; set; }
    public IEnumerable<Colour> SelectedColours { get; init; }

    public Game()
    {
        LatestPlayerGuess = Array.Empty<Colour>();
        SelectedColours = Array.Empty<Colour>();
        GuessingCount = 0;
    }

    public void IncrementGuesses()
    {
        GuessingCount++;
    }
}