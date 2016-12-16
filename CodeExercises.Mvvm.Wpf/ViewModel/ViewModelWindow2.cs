using System.Collections.ObjectModel;
using System.Windows;
using CodeExercises.Mvvm.Wpf.Data;
using CodeExercises.Mvvm.Wpf.Helpers;
using CodeExercises.Mvvm.Wpf.Model;
using CodeExercises.Mvvm.Wpf.View;

namespace CodeExercises.Mvvm.Wpf.ViewModel
{
    internal class ViewModelWindow2 : DependencyObject
    {
        // Using a DependencyProperty as the backing store for SelectedPerson.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPersonProperty =
            DependencyProperty.Register("SelectedPerson", typeof (Person), typeof (ViewModelWindow2),
                new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for People.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PeopleProperty =
            DependencyProperty.Register("People", typeof (ObservableCollection<Person>), typeof (ViewModelWindow2),
                new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for CloseWindowFlag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseWindowFlagProperty =
            DependencyProperty.Register("CloseWindowFlag", typeof (bool?), typeof (ViewModelWindow2),
                new UIPropertyMetadata(null));

        public ViewModelWindow2()
        {
            People = FakeDatabaseLayer.GetPeopleFromDatabase();
            NextExampleCommand = new RelayCommand(NextExample, NextExample_CanExecute);
        }

        //just type propdp in VisualStudio, below this line, then press Tab to get the DependencyProperty snippet

        public Person SelectedPerson
        {
            get { return (Person) GetValue(SelectedPersonProperty); }
            set { SetValue(SelectedPersonProperty, value); }
        }

        public ObservableCollection<Person> People
        {
            get { return (ObservableCollection<Person>) GetValue(PeopleProperty); }
            set { SetValue(PeopleProperty, value); }
        }

        public bool? CloseWindowFlag
        {
            get { return (bool?) GetValue(CloseWindowFlagProperty); }
            set { SetValue(CloseWindowFlagProperty, value); }
        }

        public RelayCommand NextExampleCommand { get; set; }

        private bool NextExample_CanExecute(object parameter)
        {
            return SelectedPerson != null;
        }

        private void NextExample(object parameter)
        {
            var win = new Window3(SelectedPerson);
            win.Show();
            CloseWindowFlag = true;
        }
    }
}