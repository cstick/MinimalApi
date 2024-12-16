namespace Web.Models;

/// <summary>
/// Size, shape and characteristics of common battery types.
/// </summary>
public record Battery : BatteryDefinition
{
    /// <inheritdoc/>
    public required string Name { get; set; }
}
