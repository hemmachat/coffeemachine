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
        public static string PROMPT_MESSAGE = "Press 'y' for Yes, 'n' for No: ";
        public static string SIZE_MESSAGE = "Do you want a large size of coffee?";
        public static string MILK_MESSAGE = "Do you want to add milk to your coffee?";
        public static string SUGAR_MESSAGE = "How many sugars?";
        private const int MIN_SUGAR = 0;
        private const int MAX_SUGAR = 2;
        public static string SUGAR_RANGE_MESSAGE = $"Choose from {MIN_SUGAR} to {MAX_SUGAR}: ";
        public static string FOAM_MESSAGE = "Foam the milk?";
        public static string SPRINKLE_MESSAGE = "Add chocolate sprinkles?";
        public static string COST_MESSAGE = "Your coffee cost is: $";

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

            if (HasValidBoolean(largeInput, out isLarge))
            {
                coffee.IsLarge = isLarge;
            }

            // milk
            var milkInput = io.ShowPrompt(MILK_MESSAGE, PROMPT_MESSAGE);
            var hasMilk = false;

            if (HasValidBoolean(milkInput, out hasMilk))
            {
                coffee.HasMilk = hasMilk;
            }

            // sugar
            var sugarInput = io.ShowPrompt(SUGAR_MESSAGE, SUGAR_RANGE_MESSAGE);
            var numberOfSugar = 0;

            if (HasValidSugar(sugarInput, out numberOfSugar))
            {
                coffee.NumberOfSugar = numberOfSugar;
            }

            // foam
            var foamInput = io.ShowPrompt(FOAM_MESSAGE, PROMPT_MESSAGE);
            var milkIsFoamed = false;

            if (HasValidBoolean(foamInput, out milkIsFoamed))
            {
                coffee.MilkIsFoamed = milkIsFoamed;
            }

            // sprinkle
            var chocolateInput = io.ShowPrompt(SPRINKLE_MESSAGE, PROMPT_MESSAGE);
            var hasSprinkles = false;

            if (HasValidBoolean(chocolateInput, out hasSprinkles))
            {
                coffee.HasSprinkles = hasSprinkles;
            }
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

        public void ShowCost(double cost, bool testMode = false)
        {
            io.ShowFinalMessage(COST_MESSAGE + cost, testMode);
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
