using System;

namespace CodeExercises.BuilderPattern
{
    internal class Program
    {
        private static void Main()
        {
            var computerShop = new ComputerShop();

            ComputerBuilder computerBuilder = new LaptopBuilder();
            computerShop.ConstructComputer(computerBuilder);
            computerBuilder.Computer.DisplayConfiguration();

            computerBuilder = new DesktopBuilder();
            computerShop.ConstructComputer(computerBuilder);
            computerBuilder.Computer.DisplayConfiguration();

            computerBuilder = new AppleBuilder();
            computerShop.ConstructComputer(computerBuilder);
            computerBuilder.Computer.DisplayConfiguration();
            Console.ReadKey();
        }
    }
}