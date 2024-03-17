namespace DateAppCSharp.Utils;

public enum WesternZodiac
{
    Capricorn = 0,
    Aquarius,
    Pisces,
    Aries,
    Taurus,
    Gemini,
    Cancer,
    Leo,
    Virgo,
    Libra,
    Scorpio,
    Sagittarius
}

public static class WesternZodiacHelper
{
    public static WesternZodiac FromDate(DateTime date)
    {
        int[] pivots = {21, 20, 21, 21, 22, 22, 24, 24, 24, 24, 23, 22};
        var month = date.Month - 1;
        var offset = date.Day < pivots[month] ? 0 : 1;
        return (WesternZodiac)((month + offset) % 12);
    }
}