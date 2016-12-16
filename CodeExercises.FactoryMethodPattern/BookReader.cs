using System;

namespace CodeExercises.FactoryMethodPattern
{
    public abstract class BookReader
    {
        protected BookReader()
        {
            Book = BuyBook();
        }

        public Book Book { get; set; }
        public abstract Book BuyBook();

        public void DisplayOwnedBooks()
        {
            Console.WriteLine(Book.GetType().ToString());
        }
    }
}