using System;
using System.Windows;
using CodeExercises.Mvvm.Wpf.Model;
using CodeExercises.Mvvm.Wpf.ViewModel;

namespace CodeExercises.Mvvm.Wpf.View
{
    public partial class Window3 : Window
    {
        public Window3(Person person)
        {
            InitializeComponent();
            var vm = new ViewModelWindow3(person);
            DataContext = vm;
            vm.CloseWindowEvent += vm_CloseWindowEvent;
        }

        private void vm_CloseWindowEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}