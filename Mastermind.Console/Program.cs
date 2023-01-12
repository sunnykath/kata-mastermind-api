using Mastermind.Application.Randomizer;
using Mastermind.Console.InputOutput;

namespace Mastermind.Console;

public static class Program
{
    public static void Main()
    {
        var console = new ConsoleInputOutput();

        var randomizer = new DefaultRandomizer();
        var mastermind = new Mastermind(console, randomizer);
            
        mastermind.PlayGame();
    }
}