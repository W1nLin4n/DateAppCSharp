namespace DateAppCSharp.Utils;

public enum ChineseZodiac
{
    Rat = 0,
    Ox, 
    Tiger, 
    Rabbit, 
    Dragon, 
    Snake, 
    Horse, 
    Goat, 
    Monkey, 
    Rooster, 
    Dog,
    Pig
}

public static class ChineseZodiacHelper
{
    public static ChineseZodiac FromDate(DateTime date)
    {
        return (ChineseZodiac)((date.Year - 4) % 12);
    }
}