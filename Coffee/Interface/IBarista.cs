using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Interface
{
    public interface IBarista
    {
        void AskCustomer();
        double CalculateCost();
        void ShowCost(double cost, bool testMode = false);
    }
}
