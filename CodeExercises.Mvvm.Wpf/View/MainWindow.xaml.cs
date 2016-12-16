using System.Windows;
using CodeExercises.Mvvm.Wpf.ViewModel;

namespace CodeExercises.Mvvm.Wpf.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window1 { DataContext = new ViewModelWindow1(tb1.Text) };
            win.Show();
            this.Close();
        }
    }
}
