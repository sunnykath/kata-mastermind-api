namespace Mastermind.Domain.Models;

public class Game
{
    public GameStatus GameState { get; set; }
    public List<Clue> Clues { get; set; }
    public int GuessingCount { get; set; }
    public Colour[] LatestPlayerGuess { get; set; }
    public Colour[] SelectedColours { get; set; }

    public Game()
    {
        LatestPlayerGuess = Array.Empty<Colour>();
        SelectedColours = Array.Empty<Colour>();
        Clues = new List<Clue>();
        GuessingCount = 0;
    }
    
}