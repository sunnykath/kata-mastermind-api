using Mastermind.Presentation;
using Mastermind.Presentation.InputOutput;

namespace Mastermind;

public class Controller
{
    private View _view;
    
    public Controller(IInputOutput inputOutput)
    {
        _view = new View(inputOutput);
    }

    public void Initialise()
    {
        _view.DisplayInitialMessage();
    }
}