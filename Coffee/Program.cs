using Autofac;
using System;
using CoffeeMachine.Interface;

namespace CoffeeMachine
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
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
            }
        }
    }
}