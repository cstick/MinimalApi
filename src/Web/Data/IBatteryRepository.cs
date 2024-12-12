using Web.Models;

namespace Web.Data;

/// <summary>
/// A repository of batteries.
/// </summary>
public interface IBatteryRepository
{
    /// <summary>
    /// Add a battery.
    /// </summary>
    /// <exception cref="InvalidOperationException">The battery already exists.</exception>
    void AddBattery(Battery battery);

    /// <summary>
    /// Does a battery with the given name exist?
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    bool DoesBatteryExist(string name);

    /// <summary>
    /// Find a battery.
    /// </summary>
    /// <param name="battery">A battery containing the values of properties to search wtih.</param>
    /// <returns>Any batteries found.</returns>
    IEnumerable<Battery> Find(Battery battery);

    /// <summary>
    /// Get a battery by its name.
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    /// <returns>The battery if found.</returns>
    Battery? Get(string name);

    /// <summary>
    /// Delete a battery.
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    void Delete(string name);
}