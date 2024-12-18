namespace Web.Models;

/// <summary>
/// Size, shape and characteristics of common battery types.
/// </summary>
public record BatteryDefinition
{
    /// <inheritdoc/>
    public required string IecName { get; set; }

    /// <inheritdoc/>
    public required string AnsiName { get; set; }

    /// <inheritdoc/>
    public required decimal Voltage { get; set; }
}
