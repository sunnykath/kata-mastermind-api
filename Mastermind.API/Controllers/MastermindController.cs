using Mastermind.API.DTO;
using Mastermind.Application.Randomizer;
using Mastermind.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Mastermind.API.Controllers;

[ApiController]
[Route("/game")]
public class MastermindController : ControllerBase
{
    private readonly Application.Controller _controller;
    private readonly MastermindContext _context;
    public MastermindController(IRandomizer randomizer, MastermindContext mastermindContext)
    {
        _controller = new Application.Controller(randomizer);
        _context = mastermindContext;
    }

    [HttpGet]
    public ActionResult<string> GetAllGames()
    {
        var games = _context.Games.Select(GameDto.ToDto).ToList();
        return Ok(games);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameDto>> GetGameById(Guid id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return Ok(GameDto.ToDto(game));
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame()
    {
        var newGame = _controller.StartNewGame();
        var game = await _context.Games.AddAsync(newGame);
        var newGameDto = GameDto.ToDto(game.Entity);
        await _context.SaveChangesAsync();

        return Created($"/game/{newGame.Id}", newGameDto);
    }
    
    [HttpPut]
    public async Task<ActionResult<GameDto>> UpdateLastPlayerGuessInGame(GameDto gameDto)
    {
        if (gameDto == null)
        {
            return BadRequest("Invalid Object");
        }
        if (gameDto.LatestPlayerGuess.Count() != 4)
        {
            return BadRequest("Guess should only contain four colours");
        }
        
        var game = await _context.Games.FindAsync(gameDto.Id);
        if (game == null)
        {
            return NotFound();
        }

        game.LatestPlayerGuess = gameDto.LatestPlayerGuess;
        var updatedGame = _controller.UpdateGameWithLastPlayerGuess(game);

        game = updatedGame;
        
        await _context.SaveChangesAsync();

        return Ok(GameDto.ToDto(game));
    }
}