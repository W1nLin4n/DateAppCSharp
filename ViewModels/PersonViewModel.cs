using System.Windows;
using DateAppCSharp.Common;
using DateAppCSharp.Models;
using DateAppCSharp.Utils;

namespace DateAppCSharp.ViewModels;

public class PersonViewModel : BindableBase
{
    private readonly Person _person = new Person();

    public PersonViewModel()
    {
        _person.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        _person.ErrorsChanged += (s, e) =>
        {
            if (_person.IsValid)
                ClearDataErrors(nameof(_person));
            foreach (string error in _person.GetErrors(null))
            {
                AddDataError(nameof(_person), error);
            }

            foreach (string error in GetErrors(null))
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        };
    }

    public DateTime BirtDate
    {
        get => _person.BirthDate;
        set => _person.BirthDate = value;
    }
    public int Age => _person.Age;
    public string ChineseZodiac => _person.ChineseZodiac.ToString();
    public string WesternZodiac => _person.WesternZodiac.ToString();
    public bool IsBirthday => _person.IsBirthday;
}