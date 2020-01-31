using System.Collections.Generic;

namespace CoreExcercises
{
    public class Employee
    {
        public Employee(string name)
        {
            Name = name;
            Reports = new List<Employee>();
        }

        public string Name { get; set; }
        public List<Employee> Reports { get; }

        public void ReportsTo(Employee employee)
        {
            Reports.Add(employee);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
