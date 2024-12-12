namespace Web.Models;

/// <summary>
/// A collection of battery constants.
/// </summary>
public static class Batteries
{
    public static Battery AA { get; set; } = new()
    {
        Name = "AA",
        IecName = "R6",
        AnsiName = "15A",
        Voltage = 1.5
    };

    public static Battery AAA { get; set; } = new()
    {
        Name = "AAA",
        IecName = "R03",
        AnsiName = "24A",
        Voltage = 1.5
    };

    public static Battery AAAA { get; set; } = new()
    {
        Name = "AAAA",
        IecName = "R8D425",
        AnsiName = "25A",
        Voltage = 1.5
    };

    public static Battery C { get; set; } = new()
    {
        Name = "C",
        IecName = "R14",
        AnsiName = "14A",
        Voltage = 1.5
    };

    public static Battery D { get; set; } = new()
    {
        Name = "D",
        IecName = "R20",
        AnsiName = "13A",
        Voltage = 3.0
    };
}