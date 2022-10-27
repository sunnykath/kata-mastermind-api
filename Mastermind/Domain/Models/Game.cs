namespace Mastermind.Domain.Models;

public class Game
{
    public List<Clue> Clues { get; set; }
    public bool HasWonGame { get; set; }
    public int GuessingCount { get; set; }
    public Colour[] SelectedColours { get; set; }

    public Game()
    {
        SelectedColours = Array.Empty<Colour>();
        Clues = new List<Clue>();
        HasWonGame = false;
        GuessingCount = 0;
    }
    
}