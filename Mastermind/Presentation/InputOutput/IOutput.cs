namespace Mastermind.Presentation.InputOutput;

public interface IOutput
{
    public void OutputWelcomeMessage();

    public void OutputColourArray(string[] colours);

    public void OutputClues(List<string> clues);

    public void OutputGameWonMessage();

    public void OutputGameQuitMessage();
    
    public void OutputGameLostMessage();

    public void OutputGuessesRemaining(int guessesRemaining);

}