using System.Collections;
using System.ComponentModel;
using System.Windows.Documents;

namespace DateAppCSharp.Common;

public class BindableBase : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _dataErrors = new();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool IsValid => !HasErrors;
    public bool HasErrors => _dataErrors.Count > 0;
    
    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == null)
            return _dataErrors.Values.SelectMany(x => x);
        
        return _dataErrors.TryGetValue(propertyName, out var value) ? value : new List<string>();
    }

    protected bool UpdateProperty<T>(ref T property, T value, string propertyName)
    {
        return UpdatePropertyValidated(ref property, value, null, propertyName);
    }
    
    protected bool UpdatePropertyValidated<T>(ref T property, T value, Action? validator, string propertyName)
    {
        if (Equals(property, value))
            return false;

        property = value;

        var wasValid = IsValid;
        validator?.Invoke();
        if (wasValid != IsValid)
            OnPropertyChanged(nameof(IsValid));
        
        OnPropertyChanged(propertyName);
        
        return true;
    }

    protected void AddDataError(string propertyName, string error)
    {
        if (!_dataErrors.TryGetValue(propertyName, out var errors))
        {
            errors = new List<string>();
            _dataErrors.Add(propertyName, errors);
        }

        if (errors.Contains(error)) return;
        errors.Add(error);
        OnErrorsChanged(propertyName);
    }

    protected void ClearDataErrors(string propertyName)
    {
        if (_dataErrors.Remove(propertyName))
            OnErrorsChanged(propertyName);
    }
    
    protected void OnPropertyChanged(string? propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnErrorsChanged(string? propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}