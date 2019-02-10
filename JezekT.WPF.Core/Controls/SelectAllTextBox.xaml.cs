using System.Windows;
using System.Windows.Controls;

namespace JezekT.WPF.Core.Controls
{
    public partial class SelectAllTextBox
    {
        public SelectAllTextBox()
        {
            AddHandler(GotKeyboardFocusEvent, new RoutedEventHandler(_selectAllText), true);
            AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(_selectAllText), true);

            InitializeComponent();
        }

        private static void _selectAllText(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }
    }
}
