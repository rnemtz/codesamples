using System;
using System.Collections.Generic;

namespace CodeExercises.Mvc.Data.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
