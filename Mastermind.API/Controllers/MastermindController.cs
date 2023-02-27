using Microsoft.AspNetCore.Mvc;

namespace Mastermind.API.Controllers;

[ApiController]
[Route("/")]
public class MastermindController : ControllerBase
{

    [HttpGet(Name = "Mastermind")]
    public ActionResult<string> Get()
    {
        return Ok("Welcome to Mastermind - by Suyash");
    }
}