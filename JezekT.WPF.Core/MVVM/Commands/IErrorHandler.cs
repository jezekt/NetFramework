using System;

namespace JezekT.WPF.Core.MVVM.Commands
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
