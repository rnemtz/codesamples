namespace CodeExercises.BuilderPattern
{
    public class LaptopBuilder : ComputerBuilder
    {
        public LaptopBuilder()
        {
            Computer = new Computer(ComputerType.Laptop);
        }

        public override void BuildMotherboard()
        {
            Computer.MotherBoard = "DELL MotherBoard";
        }

        public override void BuildProcessor()
        {
            Computer.Processor = "Intel Core 2 Duo";
        }

        public override void BuildHardDisk()
        {
            Computer.HardDisk = "250GB";
        }

        public override void BuildScreen()
        {
            Computer.Screen = "15.4-inch (1280 x 800)";
        }
    }
}