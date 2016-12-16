using System;

namespace CodeExercises.Abstract
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual bool CanEat()
        {
            return true;
        }

        public virtual string FullName()
        {
            return String.Format("Full Name is: {0} {1}", Name, LastName);
        }
    }
}