namespace CoffeeMachine.Interface
{
    public interface IInputOutput
    {
        string ShowPrompt(string message, string promptMessage);
        void ShowInvalidInput(string message);
        void ShowFinalMessage(string message);
    }
}