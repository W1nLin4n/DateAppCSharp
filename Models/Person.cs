using System.Collections;
using System.ComponentModel;
using DateAppCSharp.Common;
using DateAppCSharp.Utils;

namespace DateAppCSharp.Models;

public class Person : BindableBase
{
    private DateTime _birthDate;
    private int _age;
    private bool _isBirthday;
    private ChineseZodiac _chineseZodiac;
    private WesternZodiac _westernZodiac;

    public Person(DateTime birthDate)
    {
        BirthDate = birthDate;
    }

    public Person() : this(DateTime.Today)
    {
        
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (!UpdatePropertyValidated(ref _birthDate, value, ValidateBirthDate, nameof(BirthDate))) return;
            Age = AgeFromDate(BirthDate);
            IsBirthday = IsBirthdayFromDate(BirthDate);
            ChineseZodiac = ChineseZodiacHelper.FromDate(BirthDate);
            WesternZodiac = WesternZodiacHelper.FromDate(BirthDate);
        }
    }
    
    public int Age
    {
        get => _age;
        set => UpdateProperty(ref _age, value, nameof(Age));
    }
    
    public bool IsBirthday
    {
        get => _isBirthday;
        set => UpdateProperty(ref _isBirthday, value, nameof(IsBirthday));
    }

    public ChineseZodiac ChineseZodiac
    {
        get => _chineseZodiac;
        set => UpdateProperty(ref _chineseZodiac, value, nameof(ChineseZodiac));
    }
    
    public WesternZodiac WesternZodiac
    {
        get => _westernZodiac;
        set => UpdateProperty(ref _westernZodiac, value, nameof(WesternZodiac));
    }
    
    private void ValidateBirthDate()
    {
        ClearDataErrors(nameof(BirthDate));
        var age = AgeFromDate(BirthDate);
        switch (age)
        {
            case < 0:
                AddDataError(nameof(BirthDate), "Age cannot be negative");
                break;
            case > 135:
                AddDataError(nameof(BirthDate), "Age cannot be greater than 135");
                break;
        }
    }

    private static int AgeFromDate(DateTime date)
    {
        var age = DateTime.Now.Year - date.Year;
        if (DateTime.Now.Month < date.Month || (DateTime.Now.Month == date.Month && DateTime.Now.Day < date.Day))
            --age;
        return age;
    }

    private static bool IsBirthdayFromDate(DateTime date)
    {
        return DateTime.Today.Month == date.Month && DateTime.Today.Day == date.Day;
    }
}