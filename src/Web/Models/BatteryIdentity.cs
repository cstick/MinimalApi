namespace Web.Models;

/// <summary>
/// Uniquely identifies a battery.
/// </summary>
public record BatteryIdentity : IBatteryStandards
{
    /// <summary>
    /// The common name of the battery.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The IEC name of the battery.
    /// </summary>
    public required string IecName { get; set; }

    /// <summary>
    /// The ANSI name of the battery.
    /// </summary>
    public required string AnsiName { get; set; }
}