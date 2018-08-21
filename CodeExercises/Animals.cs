namespace CodeExercises
{
    public abstract class Animal
    {
        public string Type { get; set; }
        public string Name { get; set; }

        private readonly int _swimDistancePerUnit;
        private readonly int _walkDistancePerUnit;
        public int WalkedDistance { get; private set; }
        public int SwimDistance { get; private set; }

        protected Animal(int swimDistancePerUnit, int walkDistancePerUnit)
        {
            _swimDistancePerUnit = swimDistancePerUnit;
            _walkDistancePerUnit = walkDistancePerUnit;
        }

        public virtual void Walk(int units)
        {
            WalkedDistance += units * _walkDistancePerUnit;
        }
        public virtual void Swim(int units)
        {
            SwimDistance += units * _swimDistancePerUnit;
        }
    }

    public class Alligator : Animal
    {
        public Alligator(int swimDistancePerUnit, int walkDistancePerUnit) 
            : base(swimDistancePerUnit, walkDistancePerUnit)
        {
        }

        public override void Swim(int units)
        {
            base.Swim(units * 10);
        }
    }

    public class Duck : Animal
    {
        public Duck(int swimDistancePerUnit, int walkDistancePerUnit) 
            : base(swimDistancePerUnit, walkDistancePerUnit)
        {
        }
    }
}
