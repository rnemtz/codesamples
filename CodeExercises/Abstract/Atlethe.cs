using System;

namespace CodeExercises.Abstract
{
    public class Athlete : Person
    {
        public string AthleteName
        {
            get { return FullName(); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                AthleteName = value;
            }
        }

        public Athlete(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }
        public override string FullName()
        {
            return string.Format("This is the new FullName: {0} , {1}", LastName, Name);
        }
    }
}