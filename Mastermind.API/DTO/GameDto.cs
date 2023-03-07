using System.ComponentModel.DataAnnotations;
using Mastermind.Domain;

namespace Mastermind.API.DTO;

public class GameDto
{
    public Guid Id { get; set; }
    
    [EnumDataType(typeof(GameStatus))]
    public GameStatus GameState { get; set; }
    
    [EnumDataType(typeof(Clue))]
    public IEnumerable<Clue>? Clues { get; set; }
    public int GuessingCount { get; set; }
    
    [EnumDataType(typeof(Colour))]
    public IEnumerable<Colour> LatestPlayerGuess { get; set; }

    public static GameDto ToDto(Game game)
    {
        return new GameDto()
        {
            Id = game.Id,
            GameState = game.GameState,
            Clues = game.Clues,
            GuessingCount = game.GuessingCount,
            LatestPlayerGuess = game.LatestPlayerGuess,
        };
    }

    public static Game ToDomain(GameDto gameDto)
    {
        return new Game()
        {
            Id = gameDto.Id,
            GameState = gameDto.GameState,
            Clues = gameDto.Clues,
            GuessingCount = gameDto.GuessingCount,
            LatestPlayerGuess = gameDto.LatestPlayerGuess,
        };
    }
    
}