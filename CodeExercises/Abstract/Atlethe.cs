using System;

namespace CodeExercises.Abstract
{
    public class Athlete : Person
    {
        public Athlete(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public string AthleteName
        {
            get { return FullName(); }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                AthleteName = value;
            }
        }

        public override string FullName()
        {
            return $"This is the new FullName: {LastName} , {Name}";
        }
    }
}