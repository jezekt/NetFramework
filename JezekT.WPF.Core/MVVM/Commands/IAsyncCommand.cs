using System.Threading.Tasks;
using System.Windows.Input;

namespace JezekT.WPF.Core.MVVM.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
