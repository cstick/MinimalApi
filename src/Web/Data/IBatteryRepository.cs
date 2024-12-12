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
    Task AddBattery(Battery battery, CancellationToken cancellationToken = default);

    /// <summary>
    /// Does a battery with the given name exist?
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    Task<bool> DoesBatteryExist(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find a battery.
    /// </summary>
    /// <param name="battery">A battery containing the values of properties to search wtih.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>Any batteries found.</returns>
    Task<IEnumerable<Battery>> Find(Battery battery, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a battery by its name.
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>The battery if found.</returns>
    Task<Battery?> Get(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a battery.
    /// </summary>
    /// <param name="name">The name of a battery.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    Task Delete(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert or update a battery.
    /// </summary>
    Task Upsert(Battery battery, CancellationToken cancellationToken = default);
}