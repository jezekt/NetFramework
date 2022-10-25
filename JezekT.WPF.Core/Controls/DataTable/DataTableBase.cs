using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace JezekT.WPF.Core.Controls.DataTable
{
    public class DataTableBase : UserControl
    {
        private readonly DataGrid _dataGrid;
        private readonly DataTableButtons _dataTableButtons;
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(ObservableCollection<DataGridColumn>), typeof(DataTableBase), new PropertyMetadata((object)null));
        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(nameof(Buttons), typeof(ObservableCollection<Button>), typeof(DataTableBase), new PropertyMetadata((object)null));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(DataTableBase), new PropertyMetadata((object)null));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(DataTableBase), new PropertyMetadata((object)null));

        public ObservableCollection<DataGridColumn> Columns
        {
            get => (ObservableCollection<DataGridColumn>)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        public ObservableCollection<Button> Buttons
        {
            get => (ObservableCollection<Button>)GetValue(ButtonsProperty);
            set => SetValue(ButtonsProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }


        protected DataTableBase()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());

            _dataTableButtons = new DataTableButtons();
            var label = new Label();
            label.SetBinding(ContentProperty, new Binding { Path = new PropertyPath("ViewTitle", Array.Empty<object>()) });

            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(_dataTableButtons);

            _dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                CanUserDeleteRows = false,
                CanUserAddRows = false,
                IsReadOnly = true,
                SelectionMode = DataGridSelectionMode.Single,
                SelectionUnit = DataGridSelectionUnit.FullRow
            };
            _dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Path = new PropertyPath("Items", Array.Empty<object>()) });
            _dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding { Path = new PropertyPath(nameof(SelectedItem), Array.Empty<object>()) });

            var style = new Style(typeof(DataGridRow));
            style.Setters.Add(new EventSetter(MouseDoubleClickEvent, new MouseButtonEventHandler(_onRowDoubleClick)));
            _dataGrid.RowStyle = style;
            Grid.SetRow(stackPanel, 0);
            grid.Children.Add(stackPanel);
            Grid.SetRow(_dataGrid, 1);
            grid.Children.Add(_dataGrid);
            
            Content = grid;
            SetValue(ColumnsProperty, new ObservableCollection<DataGridColumn>());
            SetValue(ButtonsProperty, new ObservableCollection<Button>());
            Columns.CollectionChanged += _onColumnsChanged;
            Buttons.CollectionChanged += _onButtonsChanged;
         
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Closing += _dispose;
            }
        }


        private void _onRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_dataTableButtons.EditButton.Command != null && _dataTableButtons.EditButton.Command.CanExecute(null))
            {
                _dataTableButtons.EditButton.Command.Execute(null);
            }
        }

        private void _onColumnsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (DataGridColumn newItem in e.NewItems)
                {
                    _dataGrid.Columns.Add(newItem);
                }
            }
        }

        private void _onButtonsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Button newItem in e.NewItems)
                {
                    _dataTableButtons.CustomButtons.Add(newItem);
                }
            }
        }

        private void _dispose(object sender, CancelEventArgs e)
        {
            Columns.CollectionChanged -= _onColumnsChanged;
            Buttons.CollectionChanged -= _onButtonsChanged;
        }
    }
}
