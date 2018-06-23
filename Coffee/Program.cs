using Autofac;
using System;
using CoffeeMachine.Interface;

namespace CoffeeMachine
{
    public class Program
    {
        private static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Coffee>().As<ICoffee>();
            builder.RegisterType<ConsoleInputOutput>().As<IInputOutput>();
            builder.RegisterType<Barista>().As<IBarista>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var barista = scope.Resolve<IBarista>();
                barista.AskCustomer();

                if (args != null && args.Length != 0 && args[0] == "test")
                {
                    barista.ShowCost(barista.CalculateCost(), true);
                }
                else
                {
                    barista.ShowCost(barista.CalculateCost());
                }
            }
        }
    }
}
