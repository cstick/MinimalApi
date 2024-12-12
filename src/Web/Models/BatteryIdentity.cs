namespace Web.Models;

/// <summary>
/// Uniquely identifies a battery.
/// </summary>
public interface IBatteryIdentity
{
    /// <summary>
    /// The common name of the battery.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The IEC name of the battery.
    /// </summary>
    public string IECName { get; set; }

    /// <summary>
    /// The ANSI name of the battery.
    /// </summary>
    public string ANSIName { get; set; }
}