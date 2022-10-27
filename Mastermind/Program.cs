﻿using Mastermind.Presentation.InputOutput;
using Mastermind.Randomizer;

namespace Mastermind
{
    public static class Program
    {
        public static void Main()
        {
            var console = new ConsoleInputOutput();

            var randomizer = new DefaultRandomizer();
            var controller = new MastermindService(console);
            
            controller.PlayGame(randomizer);

        }
    }
    
}