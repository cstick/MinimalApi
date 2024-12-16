namespace Web.Models;

/// <summary>
/// Size, shape and characteristics of common battery types.
/// </summary>
public record BatteryDefinition : IBatteryStandards, IBatterySpecification
{
    /// <inheritdoc/>
    public string IecName { get; set; } = string.Empty;

    /// <inheritdoc/>
    public string AnsiName { get; set; } = string.Empty;

    /// <inheritdoc/>
    public double Voltage { get; set; }
}
