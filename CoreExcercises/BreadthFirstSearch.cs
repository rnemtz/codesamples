using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreExcercises
{
    public class BreadthFirstSearch
    {
        private readonly Employee RootEmployee;

        public BreadthFirstSearch()
        {
            var eva = new Employee("Eva");
            var sophia = new Employee("Sophia");
            var brian = new Employee("Brian");
            eva.ReportsTo(sophia);
            eva.ReportsTo(brian);

            var lisa = new Employee("Lisa");
            var tina = new Employee("Tina");
            var john = new Employee("John");
            var mike = new Employee("Mike");
            
            sophia.ReportsTo(lisa);
            sophia.ReportsTo(john);
            brian.ReportsTo(tina);
            brian.ReportsTo(mike);

            RootEmployee = eva;
        }

        public Employee Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var q = new Queue<Employee>();
            q.Enqueue(RootEmployee);

            while (q.Any())
            {
                var current = q.Dequeue();
                if (current.Name == name)
                {
                    return current;
                }

                foreach (var e in current.Reports)
                {
                    q.Enqueue(e);
                }
            }

            return null;
        }

        public List<string> GetEmployeeList()
        {
            var list = new List<string>();
            if (RootEmployee == null)
            {
                return list;
            }

            var q = new Queue<Employee>();
            q.Enqueue(RootEmployee);

            while (q.Any())
            {
                var current = q.Dequeue();
                list.Add(current.Name);
                foreach (var employee in current.Reports)
                {
                    q.Enqueue(employee);
                }
            }

            return list;
        }
    }
}
