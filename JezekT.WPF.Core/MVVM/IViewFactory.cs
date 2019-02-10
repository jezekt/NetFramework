using System.Windows;
using JezekT.WPF.Core.MVVM.ViewModels;

namespace JezekT.WPF.Core.MVVM
{
    public interface IViewFactory
    {
        Window CreateView(ViewModelBase viewModel);
    }
}
