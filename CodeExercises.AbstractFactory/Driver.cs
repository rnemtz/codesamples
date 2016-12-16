namespace CodeExercises.AbstractFactory
{
    public class Driver
    {
        private readonly FamilyCar _familyCar;
        private readonly SportsCar _sportsCar;

        public Driver(CarFactory carFactory)
        {
            _sportsCar = carFactory.CreateSportsCar();
            _familyCar = carFactory.CreateFamilyCar();
        }

        public void CompareSpeed()
        {
            _familyCar.Speed(_sportsCar);
        }
    }
}