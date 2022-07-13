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

        public void DisplayGameDetails()
        {
            _inputOutput.DisplayOutput(Constants.Title);
            _inputOutput.DisplayOutput(Constants.GameInformation);
        }

        public void GetUserGuess()
        {
            _inputOutput.DisplayOutput(Constants.GetInputPrompt);
            _inputOutput.DisplayOutput(Constants.DefaultColourRow);
        }
    }
}