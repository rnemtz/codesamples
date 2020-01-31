using System;
using System.Collections.Generic;

namespace CoreExcercises
{
    public class DepthFirstSearch
    {
        private readonly Employee RootEmployee;

        public DepthFirstSearch()
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

            return Search(RootEmployee, name);
        }

        private static Employee Search(Employee employee, string name)
        {
            if (name == employee.Name)
            {
                return employee;
            }

            Employee personFound = null;
            foreach (var employeeReport in employee.Reports)
            {
                personFound = Search(employeeReport, name);
                if (personFound != null)
                {
                    break;
                }
            }
            return personFound;
        }

        public List<string> GetEmployeeList()
        {
            var list = new List<string>();
            if (RootEmployee == null)
            {
                return list;
            }
            
            Fill(RootEmployee, list);

            return list;
        }

        private static void Fill(Employee employee, ICollection<string> list)
        {
            list.Add(employee.Name);
            foreach (var employeeReport in employee.Reports)
            {
                Fill(employeeReport, list);
            }
        }
    }
}
