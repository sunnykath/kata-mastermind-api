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

    [HttpPatch("game")]
    public ActionResult<Game> GetNextGameStateObject(Game game)
    {
        // Update the game state object and return it
        var newGameStateObject = _controller.UpdateGameWithLastPlayerGuess(game);

        return Ok(newGameStateObject);
    }
}