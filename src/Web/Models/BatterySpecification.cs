namespace Web.Models;

/// <summary>
/// Specifications of a battery.
/// </summary>
public interface IBatterySpecification
{
    /// <summary>
    /// The nominal voltage of the battery.
    /// </summary>
    public double Voltage { get; set; }
}
