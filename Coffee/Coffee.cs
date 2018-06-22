using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Interface;

namespace CoffeeMachine
{
    public class Coffee : ICoffee
    {
        public bool IsLarge { get; set; }
        public bool HasMilk { get; set; }
        public int NumberOfSugar { get; set; }
        public bool MilkIsFoamed { get; set; }
        public bool HasSprinkles { get; set; }
    }
}
