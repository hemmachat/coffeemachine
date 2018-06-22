using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Interface;

namespace CoffeeMachine
{
    public class Barista : IBarista
    {
        private readonly IInputOutput io;
        private readonly ICoffee coffee;
        private const string PROMPT_MESSAGE = "Type 'y' for Yes or type 'n' for No: ";
        private const string SIZE_MESSAGE = "\nDo you want a large size of coffee?";
        private const string MILK_MESSAGE = "\nDo you want to add milk to your coffee?";
        private const string SUGAR_MESSAGE = "\nHow many sugars?";
        private const int MIN_SUGAR = 0;
        private const int MAX_SUGAR = 2;
        private static readonly string SUGAR_RANGE_MESSAGE = $"Too much sugar is not good for your health. Choose from {MIN_SUGAR} to {MAX_SUGAR}: ";
        private const string FOAM_MESSAGE = "\nFoam the milk?";
        private const string CHOCOLATE_MESSAGE = "\nAdd chocolate sprinkles?";

        public Barista(IInputOutput inputOutput, ICoffee coffee)
        {
            io = inputOutput;
            this.coffee = coffee;
        }

        public void AskCustomer()
        {
            // size
            var largeInput = io.ShowPrompt(SIZE_MESSAGE, PROMPT_MESSAGE);
            var isLarge = false;

            while (!HasValidBoolean(largeInput, out isLarge))
            {
                io.ShowInvalidInput($"Sorry, you have selected an invalid coffee size - '{largeInput}'.");
                largeInput = io.ShowPrompt(SIZE_MESSAGE, PROMPT_MESSAGE);
            }

            coffee.IsLarge = isLarge;

            // adding milk
            var milkInput = io.ShowPrompt(MILK_MESSAGE, PROMPT_MESSAGE);
            var hasMilk = false;

            while (!HasValidBoolean(milkInput, out hasMilk))
            {
                io.ShowInvalidInput($"Sorry, you have selected an invalid milk - '{milkInput}'.");
                milkInput = io.ShowPrompt(MILK_MESSAGE, PROMPT_MESSAGE);
            }

            coffee.HasMilk = hasMilk;

            // sugar
            var sugarInput = io.ShowPrompt(SUGAR_MESSAGE, SUGAR_RANGE_MESSAGE);
            var numberOfSugar = 0;

            while (!HasValidSugar(sugarInput, out numberOfSugar))
            {
                io.ShowInvalidInput($"Sorry, you have selected an invalid amount of sugar - '{sugarInput}'.");
                sugarInput = io.ShowPrompt(SUGAR_MESSAGE, SUGAR_RANGE_MESSAGE);
            }

            coffee.NumberOfSugar = numberOfSugar;

            // foam
            var foamInput = io.ShowPrompt(FOAM_MESSAGE, PROMPT_MESSAGE);
            var milkIsFoamed = false;

            while (!HasValidBoolean(foamInput, out milkIsFoamed))
            {
                io.ShowInvalidInput($"Sorry, you have selected an invalid foam - '{foamInput}'.");
                foamInput = io.ShowPrompt(FOAM_MESSAGE, PROMPT_MESSAGE);
            }

            coffee.MilkIsFoamed = milkIsFoamed;

            // chocolate
            var chocolateInput = io.ShowPrompt(CHOCOLATE_MESSAGE, PROMPT_MESSAGE);
            var hasSprinkles = false;

            while (!HasValidBoolean(chocolateInput, out hasSprinkles))
            {
                io.ShowInvalidInput($"Sorry, you have selected an invalid chocolate - '{chocolateInput}'.");
                chocolateInput = io.ShowPrompt(CHOCOLATE_MESSAGE, PROMPT_MESSAGE);
            }

            coffee.HasSprinkles = hasSprinkles;

            double cost = CalculateCost();
            io.ShowFinalMessage("Your coffee cost is: $" + cost);
        }

        public double CalculateCost()
        {
            // Start with basic Expresso
            double price = 1.50;

            // Milk 25c or 30c for large
            if (coffee.HasMilk)
            {
                if (coffee.IsLarge)
                {
                    price += .30;
                }
                else
                {
                    price += .25;
                }
            }

            // Calc sugar @ 10c each
            price += (0.10 * coffee.NumberOfSugar);

            // Milk Foaming 5c
            if (coffee.MilkIsFoamed)
            {
                price += .05;
            }

            // Add service charge of $1.30 per order
            price += 1.30;

            return price;
        }

        private bool HasValidSugar(string input, out int sugar)
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

        private bool HasValidBoolean(string input, out bool boolValue)
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
    }
}
