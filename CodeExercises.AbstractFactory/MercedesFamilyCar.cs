using System;

namespace CodeExercises.AbstractFactory
{
    internal class MercedesFamilyCar : FamilyCar
    {
        public override void Speed(SportsCar abstractSportsCar)
        {
            Console.WriteLine(GetType().Name + " is slower than " + abstractSportsCar.GetType().Name);
        }
    }
}