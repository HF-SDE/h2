using BenchmarkDotNet.Attributes;

var dateString = "2023-10-19T10:10:00";
Console.WriteLine(dateString);
Console.WriteLine(DateTimeFromString.YearFromString(dateString));

[MemoryDiagnoser]
public class HastighedsTest
{
    readonly string dateString = "2023-10-19T10:10:00";
    [Benchmark]
    public int YearFromString()
    {
        int year = DateTimeFromString.YearFromString(dateString);
        return year;
    }
}

static public class DateTimeFromString
{
    static public int YearFromString(string dateString)
    {
        var date = DateTime.Parse(dateString);
        return date.Year;
    }
}
