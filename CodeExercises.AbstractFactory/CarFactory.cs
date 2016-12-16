namespace CodeExercises.AbstractFactory
{
    public abstract class CarFactory
    {
        public abstract SportsCar CreateSportsCar();
        public abstract FamilyCar CreateFamilyCar();
    }
}