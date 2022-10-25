using System;
using System.Threading.Tasks;
using JezekT.WPF.Core.MVVM.Commands;

namespace JezekT.WPF.Core.MVVM.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
