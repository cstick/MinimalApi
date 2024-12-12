namespace Web.Models;

/// <summary>
/// A collection of battery constants.
/// </summary>
public static class Batteries
{
    public static Battery AA { get; set; } = new()
    {
        Name = "AA",
        IECName = "R6",
        ANSIName = "15A",
        Voltage = 1.5
    };

    public static Battery AAA { get; set; } = new()
    {
        Name = "AAA",
        IECName = "R03",
        ANSIName = "24A",
        Voltage = 1.5
    };

    public static Battery AAAA { get; set; } = new()
    {
        Name = "AAAA",
        IECName = "R8D425",
        ANSIName = "25A",
        Voltage = 1.5
    };

    public static Battery C { get; set; } = new()
    {
        Name = "C",
        IECName = "R14",
        ANSIName = "14A",
        Voltage = 1.5
    };

    public static Battery D { get; set; } = new()
    {
        Name = "D",
        IECName = "R20",
        ANSIName = "13A",
        Voltage = 3.0
    };
}