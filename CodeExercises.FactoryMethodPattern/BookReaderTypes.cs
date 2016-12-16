namespace CodeExercises.FactoryMethodPattern
{
    public class HorrorBookReader : BookReader
    {
        public override Book BuyBook()
        {
            return new Dracula();
        }
    }

    public class FantasyBookReader : BookReader
    {
        public override Book BuyBook()
        {
            return new LordOfTheRings();
        }
    }

    public class AdventureBookReader : BookReader
    {
        public override Book BuyBook()
        {
            return new TreasureIsland();
        }
    }
}