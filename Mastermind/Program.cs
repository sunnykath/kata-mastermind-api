using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;

namespace Mastermind
{
    public static class Program
    {
        public static void Main()
        {
            var console = new ConsoleInputOutput();

            var randomizer = new DefaultRandomizer();
            var mastermind = new MastermindService(console, randomizer);
            
            mastermind.PlayGame();

        }
    }
    
}