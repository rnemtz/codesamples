using System;

namespace CoreExcercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bfs = new BreadthFirstSearch();
            foreach (var c in bfs.GetEmployeeList())
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("-------------------");
            var dfs = new DepthFirstSearch();
            foreach (var c in dfs.GetEmployeeList())
            {
                Console.WriteLine(c);
            }

            Console.ReadKey();
        }
    }
}
