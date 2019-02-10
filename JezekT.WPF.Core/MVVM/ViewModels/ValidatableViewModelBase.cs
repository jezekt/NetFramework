using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JezekT.WPF.Core.MVVM.ViewModels
{
    public abstract class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Count > 0;


        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

        protected void ValidateProperty(object value, [CallerMemberName] string propertyName = null, object instance = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                instance = instance ?? this;
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                }

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(instance, null, null) { MemberName = propertyName };
                if (!Validator.TryValidateProperty(value, validationContext, validationResults))
                {
                    _errors.Add(propertyName, new List<string>());
                    foreach (var validationResult in validationResults)
                    {
                        _errors[propertyName].Add(validationResult.ErrorMessage);
                    }
                }

                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                OnErrorsChanged();
            }
        }

        protected bool ValidateModel(object instance = null)
        {
            _errors.Clear();
            instance = instance ?? this;
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(instance, null, null);
            if (!Validator.TryValidateObject(instance, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    string property = validationResult.MemberNames.ElementAt(0);
                    if (_errors.ContainsKey(property))
                    {
                        _errors[property].Add(validationResult.ErrorMessage);
                    }
                    else
                    {
                        _errors.Add(property, new List<string> { validationResult.ErrorMessage });
                    }
                }
            }

            foreach (var error in _errors)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(error.Key));
            }
            OnErrorsChanged();

            return !HasErrors;
        }

        protected virtual void OnErrorsChanged() { }


        protected ValidatableViewModelBase(ViewViewModelManager viewViewModelManager) 
            : base(viewViewModelManager)
        {
        }
    }
}
