using System.Windows;
using CodeExercises.Mvvm.Wpf.ViewModel;

namespace CodeExercises.Mvvm.Wpf.View
{
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            DataContext = new ViewModelWindow2();
        }
    }
}
