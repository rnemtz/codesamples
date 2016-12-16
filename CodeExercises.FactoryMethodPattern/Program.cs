using System;
using System.Collections.Generic;

namespace CodeExercises.FactoryMethodPattern
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bookReaderList = new List<BookReader>
            {
                new AdventureBookReader(),
                new FantasyBookReader(),
                new HorrorBookReader()
            };


            foreach (var reader in bookReaderList)
            {
                Console.WriteLine(reader.GetType().ToString());
                reader.DisplayOwnedBooks();

                Console.WriteLine();
            }

            // C# specific solution using generics
            var genericReader = new AdventureBookReader();
            var book = genericReader.BuyBook();
            Console.WriteLine(book.GetType().ToString());

            Console.ReadKey();
        }
    }
}