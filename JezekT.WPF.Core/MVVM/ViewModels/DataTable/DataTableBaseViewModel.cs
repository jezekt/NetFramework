using JezekT.WPF.Core.MVVM.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace JezekT.WPF.Core.MVVM.ViewModels.DataTable
{
    public abstract class DataTableBaseViewModel<T> : ViewModelBase where T : class
    {
        private ICommand _addNewItem;
        private ICommand _editSelectedItem;
        private IAsyncCommand _deleteSelectedItem;
        private IEnumerable<T> _items;
        private T _selectedItem;

        protected abstract ActionCommand OnAddNewItem { get; }

        protected abstract ActionCommand OnEditItem { get; }

        protected abstract AsyncCommand OnDeleteItemAsync { get; }

        public virtual string AddItemTooltip => "Add new item";

        public virtual string EditItemTooltip => "Edit item";

        public virtual string DeleteItemTooltip => "Delete item";

        public ICommand AddNewItem => _addNewItem ?? (_addNewItem = OnAddNewItem);

        public ICommand EditSelectedItem => _editSelectedItem ?? (_editSelectedItem = OnEditItem);

        public IAsyncCommand DeleteSelectedItem => _deleteSelectedItem ?? (_deleteSelectedItem = OnDeleteItemAsync);

        public IEnumerable<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                DeleteSelectedItem.RaiseCanExecuteChanged();
                OnSelectedItemChanged();
            }
        }

        protected virtual void OnSelectedItemChanged()
        {
        }

        protected DataTableBaseViewModel(ViewViewModelManager viewViewModelManager)
          : base(viewViewModelManager)
        {
        }
    }
}
