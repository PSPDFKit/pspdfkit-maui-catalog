using System.Windows.Input;

namespace PSPDFKit.Maui.Catalog.MVVM
{
    internal class AsyncCommand: ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public AsyncCommand(Func<Task> execute) : this(execute, null) { }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            if (_canExecute != null && !CanExecute(parameter))
            {
                return;
            }

            Task.Run(()=> _execute());
        }

        public event EventHandler CanExecuteChanged;

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
