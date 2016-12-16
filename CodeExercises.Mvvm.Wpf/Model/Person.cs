using System.ComponentModel;

namespace CodeExercises.Mvvm.Wpf.Model
{
    public class Person : INotifyPropertyChanged
    {
        string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age == value) return;
                _age = value;
                RaisePropertyChanged("Age");
            }
        }

        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
