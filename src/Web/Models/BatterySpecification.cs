namespace Web.Models;

/// <summary>
/// Specifications of a battery.
/// </summary>
public record BatterySpecification : IBatterySpecification
{
    /// <summary>
    /// The nominal voltage of the battery.
    /// </summary>
    public required double Voltage { get; set; }
}
