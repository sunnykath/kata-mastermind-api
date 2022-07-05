using Mastermind.Presentation.InputOutput;

namespace Mastermind.Presentation
{
    public class View
    {
        private readonly IInputOutput _inputOutput;
        public View(IInputOutput consoleInputOutput)
        {
            _inputOutput = consoleInputOutput;
        }

        public void StartGame()
        {
            _inputOutput.DisplayOutput(Constants.Title);
            _inputOutput.DisplayOutput(Constants.GameInformation);
        }
    }
}