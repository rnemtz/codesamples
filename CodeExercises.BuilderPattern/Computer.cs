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
            string message;

            message = string.Format("Computer: {0}", _computerType);
            Console.WriteLine(message);

            message = string.Format("Motherboard: {0}", MotherBoard);
            Console.WriteLine(message);

            message = string.Format("Processor: {0}", Processor);
            Console.WriteLine(message);

            message = string.Format("Harddisk: {0}", HardDisk);
            Console.WriteLine(message);

            message = string.Format("Screen: {0}", Screen);
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