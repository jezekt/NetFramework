using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace JezekT.WPF.Core.MVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public abstract string ViewTitle { get; }
        public readonly ViewViewModelManager ViewViewModelManager;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected ViewModelBase(ViewViewModelManager viewViewModelManager)
        {
            if (viewViewModelManager == null) throw new ArgumentNullException();
            Contract.EndContractBlock();

            ViewViewModelManager = viewViewModelManager;
        }
    }
}
