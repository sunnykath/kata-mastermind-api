namespace Mastermind.Presentation.InputOutput
{
    public interface IInputOutput
    {
        public void DisplayOutput(string output);
        string[] GetPlayerInput();
    }
}