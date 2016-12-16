using System;
using System.Collections.Generic;
using System.Threading;
using CodeExercises.Mvvm.Wpf.Data;

namespace CodeExercises.Mvvm.Wpf.Model
{
    internal class PersonnelBusinessObject
    {
        public enum StatusType
        {
            Offline = 0,
            Online = 1
        }

        private Timer _statusTimer;

        public PersonnelBusinessObject()
        {
            People = FakeDatabaseLayer.GetPocoPeopleFromDatabase();
            _statusTimer = new Timer(StatusChangeTick, null, 1000, 1000);
        }

        private List<PocoPerson> People { get; set; }
        public StatusType Status { get; set; }
        public string ReportTitle { get; set; }
        public event EventHandler PeopleChanged;

        private void StatusChangeTick(object state)
        {
            Status = Status == StatusType.Offline ? StatusType.Online : StatusType.Offline;
        }

        public List<PocoPerson> GetEmployees()
        {
            return People;
        }

        public void AddPerson(PocoPerson person)
        {
            People.Add(person);
            OnPeopleChanged();
        }

        public void DeletePerson(PocoPerson person)
        {
            People.Remove(person);
            OnPeopleChanged();
        }

        public void UpdatePerson(PocoPerson person)
        {
        }

        private void OnPeopleChanged()
        {
            if (PeopleChanged != null)
                PeopleChanged(this, null);
        }
    }
}