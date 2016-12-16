using System;
using System.Windows;
using CodeExercises.Mvvm.Wpf.Helpers;

namespace CodeExercises.Mvvm.Wpf.View
{
    public partial class Window4
    {
        public Window4()
        {
            InitializeComponent();
            DataContextChanged += Window4_DataContextChanged;
        }

        private void Window4_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dc = DataContext as IClosableViewModel;
            if (dc != null) dc.CloseWindowEvent += dc_CloseWindowEvent;
        }

        private void dc_CloseWindowEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}