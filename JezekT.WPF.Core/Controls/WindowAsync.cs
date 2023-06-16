using System.ComponentModel;
using System.Windows;

namespace JezekT.WPF.Core.Controls
{
    public class WindowAsync : Window
    {
        public static readonly DependencyProperty TaskRunningProperty = DependencyProperty.Register(nameof(TaskRunning), typeof(bool), typeof(WindowAsync), new PropertyMetadata(false));

        public bool TaskRunning
        {
            get => (bool) GetValue(TaskRunningProperty);
            set => SetValue(TaskRunningProperty, value);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (TaskRunning)
            {
                e.Cancel = true;
            }
        }
    }
}
