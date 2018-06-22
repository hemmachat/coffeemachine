using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class CoffeeCalculator
    {
        public double CalculateCost(Coffee coffee)
        {
            // Start with basic Expresso
            double price = 1.50;

            //Milk 25c or 30c for large
            if (coffee.HasMilk && coffee.IsLarge)
            {
                price += .30;
            }

            if (coffee.HasMilk && !coffee.IsLarge)
            {
                price += .25;
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
    }
}
