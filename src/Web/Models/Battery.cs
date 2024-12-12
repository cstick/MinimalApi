namespace Web.Models;

/// <summary>
/// Size, shape and characteristics of common battery types.
/// </summary>
public record Battery : IBatteryIdentity, IBatterySpecification
{
    /// <inheritdoc/>
    public required string Name { get; set; }
    
    /// <inheritdoc/>
    public required string IECName { get; set; }
    
    /// <inheritdoc/>
    public required string ANSIName { get; set; }

    /// <inheritdoc/>
    public double Voltage { get; set; }
}
