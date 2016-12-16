using System;

namespace CodeExercises.AbstractFactory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           
            AbstractFactory();
        }

        public static void AbstractFactory()
        {
            // Language agnostic version
            CarFactory audiFactory = new AudiFactory();
            var driver1 = new Driver(audiFactory);
            driver1.CompareSpeed();

            CarFactory mercedesFactory = new MercedesFactory();
            var driver2 = new Driver(mercedesFactory);
            driver2.CompareSpeed();

            // C# specific version using generics
            var factory = new GenericFactory<MercedesSportsCar>();
            var mercedesSportsCar = factory.CreateObject();
            Console.WriteLine(mercedesSportsCar.GetType().ToString());

            Console.ReadKey();
        }
    }
}