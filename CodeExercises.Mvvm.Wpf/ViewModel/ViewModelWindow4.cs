﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Threading;
using CodeExercises.Mvvm.Wpf.Helpers;
using CodeExercises.Mvvm.Wpf.Model;
using MvvmExample.View;

namespace CodeExercises.Mvvm.Wpf.ViewModel
{
    class ViewModelWindow4 : ViewModelBase, IClosableViewModel
    {
        public event EventHandler CloseWindowEvent;

        public List<PocoPerson> People { get; set; }

        string _textProperty1;
        public string TextProperty1
        {
            get
            {
                return _textProperty1;
            }
            set
            {
                if (_textProperty1 == value) return;
                _textProperty1 = value;
                RaisePropertyChanged("TextProperty1"); //The fix
            }
        }

        public object SelectedPerson { get; set; }

        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand NextExampleCommand { get; set; }

        public ViewModelWindow4()
        {
            People = new List<PocoPerson>
            {
                new PocoPerson { FirstName="Tom", LastName="Jones", Age=80 },
                new PocoPerson { FirstName="Dick", LastName="Tracey", Age=40 },
                new PocoPerson { FirstName="Harry", LastName="Hill", Age=60 },
            };
            TextProperty1 = "This will now update";
            NextExampleCommand = new RelayCommand(NextExample);
            AddUserCommand = new RelayCommand(AddUser);

            var timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void AddUser(object parameter)
        {
            if (parameter == null) return;
            People.Add(new PocoPerson { FirstName = parameter.ToString(), LastName = parameter.ToString(), Age = DateTime.Now.Second });
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //This simulates something happening in the background
            //These changes are NOT reflected in the UI
            //For these changes to show, you need INotifyPropertyChanged or DependencyProperty
            TextProperty1 = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        void NextExample(object parameter)
        {
            var win = new Window5 { DataContext = new ViewModelWindow5() };
            win.Show();

            if (CloseWindowEvent != null)
                CloseWindowEvent(this, null);
        }    
    }
}
