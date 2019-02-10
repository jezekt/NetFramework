using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JezekT.WPF.Core.MVVM.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null,
            IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    RaiseCanExecuteChanged();
                    await _execute().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (_errorHandler != null)
                    {
                        _errorHandler.HandleError(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    _isExecuting = false;
                    RaiseCanExecuteChanged();
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            });
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync();
        }
        #endregion
    }
}
