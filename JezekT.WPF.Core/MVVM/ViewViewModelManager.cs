using JezekT.WPF.Core.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;

namespace JezekT.WPF.Core.MVVM
{
    public class ViewViewModelManager
    {
        private readonly IViewFactory _viewFactory;
        private readonly Dictionary<ViewModelBase, Window> _viewViewModelDictionary;
        private ViewModelBase _mainViewModel;

        public void OpenWindow(ViewModelBase viewModel, bool modal = false)
        {
            if (viewModel == null) throw new ArgumentNullException();
            Contract.EndContractBlock();
            
            var view = _getView(viewModel);
            if (_viewViewModelDictionary.ContainsKey(viewModel))
            {
                if (view.WindowState == WindowState.Minimized)
                {
                    view.WindowState = WindowState.Normal;
                }
            }
            else
            {
                _viewViewModelDictionary.Add(viewModel, view);
                view.DataContext = viewModel;

                if (_mainViewModel == null)
                {
                    _mainViewModel = viewModel;
                }

                if (modal)
                {
                    view.ShowDialog();
                }
                else
                {
                    view.Show();
                }
            }
        }

        public void CloseWindow(ViewModelBase viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException();
            Contract.EndContractBlock();

            if (_viewViewModelDictionary.TryGetValue(viewModel, out var view))
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => view.Close()));
                if (viewModel is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _viewViewModelDictionary.Remove(viewModel);
            }
        }


        public ViewViewModelManager(IViewFactory viewFactory)
        {
            if (viewFactory == null) throw new ArgumentNullException();
            Contract.EndContractBlock();

            _viewFactory = viewFactory;
            _viewViewModelDictionary = new Dictionary<ViewModelBase, Window>();
        }


        private Window _getView(ViewModelBase viewModel)
        {
            Contract.Requires(viewModel != null);

            if (!_viewViewModelDictionary.TryGetValue(viewModel, out var view))
            {
                view = _viewFactory.CreateView(viewModel);
            }
            return view;
        }
    }
}
