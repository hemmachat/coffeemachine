namespace CoffeeMachine.Interface
{
    public interface IInputOutput
    {
        string ShowPrompt(string message, string promptMessage);
        void ShowFinalMessage(string message, bool testMode = false);
    }
}