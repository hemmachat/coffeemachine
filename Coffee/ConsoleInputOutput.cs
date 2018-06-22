using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Interface;

namespace CoffeeMachine
{
    public class ConsoleInputOutput : IInputOutput
    {
        public void ShowFinalMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }

        public void ShowInvalidInput(string message)
        {
            Console.WriteLine($"{message} Please try again.");
        }

        public string ShowPrompt(string message, string promptMessage)
        {
            Console.WriteLine(message);
            Console.Write(promptMessage);

            return Console.ReadLine().ToLower();
        }


    }
}
