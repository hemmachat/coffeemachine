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
        public void ShowFinalMessage(string message, bool testMode = false)
        {
            Console.WriteLine(message);

            if (!testMode)
            {
                Console.ReadKey(true);
            }
        }

        public string ShowPrompt(string message, string promptMessage)
        {
            Console.WriteLine(message);
            Console.Write(promptMessage);

            return Console.ReadLine().ToLower();
        }


    }
}
