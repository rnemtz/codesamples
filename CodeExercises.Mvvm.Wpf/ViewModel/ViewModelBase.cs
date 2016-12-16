using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace CodeExercises.Mvvm.Wpf.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        //Extra Stuff, shows why a base ViewModel is useful
        private bool? _closeWindowFlag;

        public bool? CloseWindowFlag
        {
            get { return _closeWindowFlag; }
            set
            {
                _closeWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //basic ViewModelBase
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = CloseWindowFlag == null
                    ? true
                    : !CloseWindowFlag;
            }));
        }
    }
}