using System;

namespace CoreExcercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bfs = new BreadthFirstSearch();
            bfs.Search("John");
            foreach (var c in bfs.GetEmployeeList())
            {
                Console.WriteLine(c);
            }

            Console.ReadKey();
        }
    }
}
