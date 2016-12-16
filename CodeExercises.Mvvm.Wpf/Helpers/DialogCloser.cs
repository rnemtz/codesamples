using System.Windows;

namespace CodeExercises.Mvvm.Wpf.Helpers
{
    public static class DialogCloser
    {
        private static void DialogResultChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.Close();
        }

        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof (bool?),
                typeof (DialogCloser),
                new PropertyMetadata(DialogResultChanged));
    }
}