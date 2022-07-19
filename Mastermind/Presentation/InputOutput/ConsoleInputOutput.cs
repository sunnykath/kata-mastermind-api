using System;

namespace Mastermind.Presentation.InputOutput
{
    public class ConsoleInputOutput : IInputOutput
    {
        public void DisplayOutput(string output)
        {
            Console.WriteLine(output);
        }

        public string[] GetPlayerInput()
        {
            throw new NotImplementedException();
        }
    }
}