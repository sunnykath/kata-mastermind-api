using Mastermind.Application.Randomizer;
using Mastermind.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Mastermind.API.Controllers;

[ApiController]
[Route("/")]
public class MastermindController : ControllerBase
{
    private readonly Application.Controller _controller;
    public MastermindController(IRandomizer randomizer)
    {
        _controller = new Application.Controller(randomizer);
    }

    [HttpGet(Name = "Mastermind")]
    public ActionResult<string> Get()
    {
        return Ok("Welcome to Mastermind - by Suyash");
    }

    [HttpPatch("game")]
    public ActionResult<Game> GetNextGameStateObject(Game game)
    {
        // Update the game state object and return it
        var newGameStateObject = _controller.UpdateGameWithLastPlayerGuess(game);

        return Ok(newGameStateObject);
    }
}