using System;

namespace CodeExercises.Sealed
{
    public sealed class SealedPerson
    {
        public SealedPerson(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public string Name { get; set; }
        public string LastName { get; set; }

        public bool CanEat()
        {
            return true;
        }

        public string FullName()
        {
            return String.Format("Full Name is: {0} {1}", Name, LastName);
        }
    }
}