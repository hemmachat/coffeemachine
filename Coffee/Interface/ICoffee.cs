using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Interface
{
    public interface ICoffee
    {
        bool IsLarge { get; set; }
        bool HasMilk { get; set; }
        int NumberOfSugar { get; set; }
        bool MilkIsFoamed { get; set; }
        bool HasSprinkles { get; set; }
    }
}
