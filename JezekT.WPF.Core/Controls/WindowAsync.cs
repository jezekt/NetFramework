using System.ComponentModel;
using System.Windows;

namespace JezekT.WPF.Core.Controls
{
    public class WindowAsync : Window
    {

        public static readonly DependencyProperty TaskRunningProperty = DependencyProperty.Register(
            "TaskRunning", typeof(bool), typeof(WindowAsync), new PropertyMetadata(default(bool)));

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
