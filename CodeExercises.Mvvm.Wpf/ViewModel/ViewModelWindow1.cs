using CodeExercises.Mvvm.Wpf.Helpers;
using CodeExercises.Mvvm.Wpf.Model;
using CodeExercises.Mvvm.Wpf.View;

namespace CodeExercises.Mvvm.Wpf.ViewModel
{
    class ViewModelWindow1 : ViewModelMain
    {
        public RelayCommand ChangeTextCommand { get; set; }
        public RelayCommand NextExampleCommand { get; set; }

        string _testText;
        public string TestText
        {
            get
            {
                return _testText;
            }
            set
            {
                if (_testText == value) return;
                _testText = value;
                RaisePropertyChanged("TestText");
            }
        }

        //This ViewModel is just to duplicate the last, but showing binding in code behind
        public ViewModelWindow1(string lastText)
        {
            _testText = lastText; //Using internal variable is ok here because binding hasn't happened yet
            ChangeTextCommand = new RelayCommand(ChangeText);
            NextExampleCommand = new RelayCommand(NextExample);
        }

        void ChangeText(object selectedItem)
        {
            //Setting the PUBLIC property 'TestText', so PropertyChanged event is fired
            if (selectedItem == null)
                TestText = "Please select a person"; 
            else
            {
                var person = selectedItem as Person;
                TestText = person.FirstName + " " + person.LastName;
            }
        }

        void NextExample(object parameter)
        {
            var win = new Window2();
            win.Show();
            CloseWindow();
        }
    }
}
