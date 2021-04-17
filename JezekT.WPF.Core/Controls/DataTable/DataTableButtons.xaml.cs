using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace JezekT.WPF.Core.Controls.DataTable
{
    public partial class DataTableButtons
    {
        public static readonly DependencyProperty CustomButtonsProperty = DependencyProperty.Register(nameof(CustomButtons), typeof(ObservableCollection<Button>), typeof(DataTableButtons), new PropertyMetadata((object)null));

        public ObservableCollection<Button> CustomButtons
        {
            get => (ObservableCollection<Button>)GetValue(CustomButtonsProperty);
            set => SetValue(CustomButtonsProperty, value);
        }


        public DataTableButtons()
        {
            SetValue(CustomButtonsProperty, new ObservableCollection<Button>());
            CustomButtons.CollectionChanged += _onCustomButtonsChanged;

            InitializeComponent();
        }


        private void _onCustomButtonsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (UIElement newItem in e.NewItems)
                {
                    ButtonsPanel.Children.Add(newItem);
                }
            }
        }
    }
}
