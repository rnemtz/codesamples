using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using CodeExercises.Mvvm.Wpf.Helpers;
using CodeExercises.Mvvm.Wpf.Model;

namespace CodeExercises.Mvvm.Wpf.ViewModel
{
    internal class ViewModelWindow5 : ViewModelBase
    {
        private PersonnelBusinessObject.StatusType _boStatus;
        private ObservableCollection<PocoPerson> _people;
        private object _selectedPerson;
        private BindingGroup _updateBindingGroup;

        private readonly PersonnelBusinessObject _personnel;
            // The sealed business object (database layer, web service, etc)

        public ViewModelWindow5()
        {
            _personnel = new PersonnelBusinessObject();
            _personnel.PeopleChanged += personnel_PeopleChanged;

            CancelCommand = new RelayCommand(DoCancel);
            SaveCommand = new RelayCommand(DoSave);
            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);

            UpdateBindingGroup = new BindingGroup {Name = "Group1"};

            var checkStatusTimer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(500)};
            checkStatusTimer.Tick += CheckStatus;
            checkStatusTimer.Start();

            CheckStatus(null, null);
        }

        public ObservableCollection<PocoPerson> People
        {
            get
            {
                _people = new ObservableCollection<PocoPerson>(_personnel.GetEmployees());
                return _people;
            }
        }

        public string ReportTitle
        {
            get { return _personnel.ReportTitle; }
            set
            {
                if (_personnel.ReportTitle != value)
                {
                    _personnel.ReportTitle = value;
                    RaisePropertyChanged("ReportTitle");
                }
            }
        }

        public PersonnelBusinessObject.StatusType BoStatus
        {
            get { return _boStatus; }
            set
            {
                if (_boStatus == value) return;
                _boStatus = value;
                RaisePropertyChanged("BoStatus");
            }
        }

        public object SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson == value) return;
                _selectedPerson = value;
                RaisePropertyChanged("SelectedPerson");
            }
        }

        public int SelectedIndex { get; set; }

        public BindingGroup UpdateBindingGroup
        {
            get { return _updateBindingGroup; }
            set
            {
                if (Equals(_updateBindingGroup, value)) return;
                _updateBindingGroup = value;
                RaisePropertyChanged("UpdateBindingGroup");
            }
        }

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }

        private void CheckStatus(object sender, EventArgs e)
        {
            //Periodically checks if the property has changed
            if (_boStatus != _personnel.Status)
                BoStatus = _personnel.Status;
        }

        private void personnel_PeopleChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() => { RaisePropertyChanged("People"); }));
        }

        private void DoCancel(object param)
        {
            UpdateBindingGroup.CancelEdit();
            if (SelectedIndex == -1)
                //This only closes if new - just to show you how CancelEdit returns old values to bindings
                SelectedPerson = null;
        }

        private void DoSave(object param)
        {
            UpdateBindingGroup.CommitEdit();
            var person = SelectedPerson as PocoPerson;
            if (SelectedIndex == -1)
            {
                _personnel.AddPerson(person);
                RaisePropertyChanged("People"); // Update the list from the data source
            }
            else
                _personnel.UpdatePerson(person);

            SelectedPerson = null;
        }

        private void AddUser(object parameter)
        {
            SelectedPerson = null;
                // Unselects last selection. Essential, as assignment below won't clear other control's SelectedItems
            var person = new PocoPerson();
            SelectedPerson = person;
        }

        private void DeleteUser(object parameter)
        {
            var person = SelectedPerson as PocoPerson;
            if (SelectedIndex != -1)
            {
                _personnel.DeletePerson(person);
                RaisePropertyChanged("People"); // Update the list from the data source
            }
            else
                SelectedPerson = null; // Simply discard the new object
        }
    }
}