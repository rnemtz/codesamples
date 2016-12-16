using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.FactoryMethodPattern
{
    class Program
    {
        static void Main(string[] args)
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
            Book book = genericReader.BuyBook();
            Console.WriteLine(book.GetType().ToString());

            Console.ReadKey();
        }
    }
}
