namespace Backend;

public class Time
{
    //Fields
    private int _hour;
    private int _milisecond;
    private int _minute;
    private int _second;

    //Constructors //#1
    public Time()
    {
    }

    public Time(int hour) //#2
    {
        Hour = hour;
    }

    public Time(int hour, int minute) //#3
    {
        Hour = hour;
        Minute = minute;
    }

    public Time(int hour, int minute, int second) //#4
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public Time(int hour, int minute, int second, int milisecond) //#5
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Milisecond = milisecond;
    }

    //Properties
    public int Hour
    {
        get => _hour;
        set => _hour = ValidateHour(value);
    }

    public int Milisecond
    {
        get => _milisecond;
        set => _milisecond = ValidateMilisecond(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidateMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidateSecond(value);
    }

    //Public Methods

    public Time Add(Time time)
    {
        int totalMiliseconds = ToMiliseconds() + time.ToMiliseconds();
        int milisecondsPerHour = 1 * 60 * 60 * 1000;

        //1 hour = 3600000 ms. 1 minute = 60000ms. 1 second = 1000ms
        int hour = (totalMiliseconds / milisecondsPerHour) % 12; 
        int minute = (totalMiliseconds % milisecondsPerHour) / (60 * 1000);
        int second = (totalMiliseconds % (60*1000)) / 1000;
        int milisecond = totalMiliseconds % 1000;
        
        return new Time(hour, minute, second, milisecond);
    }

    public bool IsOtherDay(Time time)
    {
        //ToMiliseconds() = this.Miliseconds() like in java
        int totalMiliseconds = ToMiliseconds() + time.ToMiliseconds();
        //1 day = 86 400 000 ms
        int milisecondsPerDay = 24 * 60 * 60 * 1000;

        if (totalMiliseconds >= milisecondsPerDay)
        {
            return true;
        }
        return false;
    }
    public int ToMiliseconds() 
    {
        try
        {
            ValidateHour(Hour);
            ValidateMinute(Minute);
            ValidateSecond(Second);
            return (Hour * 3600000) + (Minute * 60000) + (Second * 1000) + Milisecond;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int ToMinutes() 
    {
        try
        {
            ValidateHour(Hour);
            ValidateMinute(Minute);

            return (Hour * 60) + Minute;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public int ToSeconds()
    {
        try
        {
            ValidateHour(Hour);
            ValidateMinute(Minute);
            ValidateSecond(Second);
            return (Hour * 3600) + (Minute * 60) + Second;
        }
        catch (Exception)
        {
            return 0;
        }
        
    }

    public override string ToString()
    {
        int hour12 = Hour % 12;

        if (hour12 == 0)
        {
            hour12 = 12;
        }
        
        string format = "";

        if (Hour < 12)
        {
            format = "AM";
        } else 
        {
            format = "PM";
        }

        return $"{hour12:D2}:{_minute:D2}:{_second:D2}:{_milisecond:D3} {format}";
    }

    //Private Methods
    private int ValidateHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new Exception($"The hour: {hour}, is not valid.");
        }

        return hour;
    }

    private int ValidateMilisecond(int milisecond)
    {

        if (milisecond < 0 || milisecond > 999)
        {
            throw new Exception($"The milisecond: {milisecond}, is not valid.");
        }

        return milisecond;
    }

    private int ValidateMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new Exception($"The minute: {minute}, is not valid.");
        }

        return minute;
    }

    private int ValidateSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new Exception($"The second: {second}, is not valid.");
        }

        return second;
    }
}
