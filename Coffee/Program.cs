using System;

namespace CoffeeMachine
{
    class Program
    {
        private const string PROMPT_MESSAGE = "Type 'y' for Yes or type 'n' for No: ";
        private const string SIZE_MESSAGE = "\nDo you want a large size of coffee?";
        private const string MILK_MESSAGE = "\nDo you want to add milk to your coffee?";
        private const string SUGAR_MESSAGE = "\nHow many sugars?";
        private const int MIN_SUGAR = 0;
        private const int MAX_SUGAR = 5;
        private static readonly string SUGAR_RANGE_MESSAGE = $"Too much sugar is not good for your health. Choose from {MIN_SUGAR} to {MAX_SUGAR}: ";
        private const string FOAM_MESSAGE = "\nFoam the milk?";
        private const string CHOCOLATE_MESSAGE = "\nAdd chocolate sprinkles?";
        static void Main(string[] args)
        {
            Coffee coffee = new Coffee();

            // size
            var largeInput = ShowPrompt(SIZE_MESSAGE);
            var isLarge = false;

            while (!HasValidBoolean(largeInput, out isLarge))
            {
                InvalidInput($"Sorry, you have selected an invalid coffee size - '{largeInput}'.");
                largeInput = ShowPrompt(SIZE_MESSAGE);
            }

            coffee.IsLarge = isLarge;

            // adding milk
            var milkInput = ShowPrompt(MILK_MESSAGE);
            var hasMilk = false;

            while (!HasValidBoolean(milkInput, out hasMilk))
            {
                InvalidInput($"Sorry, you have selected an invalid milk - '{milkInput}'.");
                milkInput = ShowPrompt(MILK_MESSAGE);
            }

            coffee.HasMilk = hasMilk;

            // sugar
            var sugarInput = ShowPrompt(SUGAR_MESSAGE, SUGAR_RANGE_MESSAGE);
            var sugar = 0;

            while (!HasValidSugar(sugarInput, out sugar))
            {
                InvalidInput($"Sorry, you have selected an invalid amount of sugar - '{sugarInput}'.");
                sugarInput = ShowPrompt(SUGAR_MESSAGE, SUGAR_RANGE_MESSAGE);
            }

            coffee.NumberOfSugar = sugar;

            // foam
            var foamInput = ShowPrompt(FOAM_MESSAGE);
            var hasFoam = false;

            while (!HasValidBoolean(foamInput, out hasFoam))
            {
                InvalidInput($"Sorry, you have selected an invalid foam - '{foamInput}'.");
                foamInput = ShowPrompt(FOAM_MESSAGE);
            }

            coffee.MilkIsFoamed = hasFoam;

            // chocolate
            var chocolateInput = ShowPrompt(CHOCOLATE_MESSAGE);
            var hasChocolate = false;

            while (!HasValidBoolean(chocolateInput, out hasChocolate))
            {
                InvalidInput($"Sorry, you have selected an invalid chocolate - '{chocolateInput}'.");
                chocolateInput = ShowPrompt(CHOCOLATE_MESSAGE);
            }

            coffee.HasSprinkles = hasChocolate;

            double cost = new CoffeeCalculator().CalculateCost(coffee);
            Console.WriteLine("Your coffee cost is: $" + cost);
            Console.ReadKey(true);
        }

        private static string ShowPrompt(string message, string promptMessage = PROMPT_MESSAGE)
        {
            Console.WriteLine(message);
            Console.Write(promptMessage);

            return Console.ReadLine().ToLower();
        }

        private static bool HasValidSugar(string input, out int sugar)
        {
            if (string.IsNullOrEmpty(input))
            {
                sugar = 0;

                return false;
            }

            var success = int.TryParse(input, out sugar);

            if (success && sugar >= MIN_SUGAR && sugar <= MAX_SUGAR)
            {
                return true;
            }
            else
            {
                sugar = 0;

                return false;
            }
        }

        private static bool HasValidBoolean(string input, out bool boolValue)
        {
            if (string.IsNullOrEmpty(input))
            {
                boolValue = false;

                return false;
            }

            if (input == "y")
            {
                input = "true";
            }
            else if (input == "n")
            {
                input = "false";
            }

            return bool.TryParse(input, out boolValue);
        }

        private static void InvalidInput(string message)
        {
            Console.WriteLine($"{message} Please try again.");
        }
    }
}