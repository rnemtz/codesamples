using System;

namespace CodeExercises.BuilderPattern
{
    public class Computer
    {
        private readonly ComputerType _computerType;

        public Computer(ComputerType computerTyp)
        {
            _computerType = computerTyp;
        }

        public string MotherBoard { get; set; }
        public string Processor { get; set; }
        public string HardDisk { get; set; }
        public string Screen { get; set; }

        public void DisplayConfiguration()
        {
            string message = $"Computer: {_computerType}";
            Console.WriteLine(message);

            message = $"Motherboard: {MotherBoard}";
            Console.WriteLine(message);

            message = $"Processor: {Processor}";
            Console.WriteLine(message);

            message = $"Harddisk: {HardDisk}";
            Console.WriteLine(message);

            message = $"Screen: {Screen}";
            Console.WriteLine(message);

            Console.WriteLine();
        }
    }

    public enum ComputerType
    {
        Laptop,
        Desktop,
        Apple
    }
}