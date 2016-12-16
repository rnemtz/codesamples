using System;

namespace CodeExercises.Mvvm.Wpf.Helpers
{
    interface IClosableViewModel
    {
        event EventHandler CloseWindowEvent;
    }
}
