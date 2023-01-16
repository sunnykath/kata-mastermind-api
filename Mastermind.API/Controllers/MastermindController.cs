using Microsoft.AspNetCore.Mvc;

namespace Mastermind.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MastermindController : ControllerBase
{

    [HttpGet(Name = "Mastermind")]
    public ActionResult<string> Get()
    {
        return Ok("Welcome to Mastermind - by Suyash");
    }

    [HttpGet(Name = "Rules")]
    public ActionResult<string> GetRules()
    {
        return Ok(@"

        Mastermind or Master Mind is a code-breaking game for two players. The modern game with pegs was invented in 
           1970 by Mordecai Meirowitz, an Israeli postmaster and telecommunications expert. It resembles an earlier 
            pencil and paper game called Bulls and Cows that may date back a century or more. (Source Wikipedia)

        Rules
        The Mastermind (computer) will select 4 colours. 
        The colours are randomly selected from [Red, Blue, Green, Orange, Purple, Yellow].
        Colours can be duplicated but there will always be exactly 4. 
        You take turns to guess what the 4 colours are.
        The Mastermind will return an array back to you with clues.
        For every correctly positioned colour in the array an element of “Black” is returned. 
        For every correct colour but in the wrong position an element of “White” will be returned.
        However, the return array will be shuffled!
            
        Passing the correct array will win you the game!
        Guessing more than 60 times will result in a loss. :(        
        ");
    }
}