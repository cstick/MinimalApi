using Web.Models;

namespace Web.Data;

/// <inheritdoc/>
internal class BatteryRepository : IBatteryRepository
{
    private static readonly IList<Battery> _batteries;

    static BatteryRepository()
    {
        _batteries = [
            Batteries.AA,
            Batteries.AAA,
            Batteries.AAAA,
            Batteries.C,
            Batteries.D,
        ];
    }

    /// <inheritdoc/>
    public async Task<bool> DoesBatteryExist(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(0, cancellationToken);

        return _batteries.Any(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc/>
    public async Task AddBattery(Battery battery, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(0, cancellationToken);

        if (_batteries.Any(b => Equals(b.Name, battery.Name)))
        {
            throw new InvalidOperationException("Battery already exists.");
        }

        _batteries.Add(battery);
    }

    /// <inheritdoc/>
    public async Task Upsert(Battery battery, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await Delete(battery.Name, cancellationToken);
        _batteries.Add(battery);
    }

    /// <inheritdoc/>
    public async Task<Battery?> Get(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(0, cancellationToken);

        return _batteries
            .FirstOrDefault(b => string.Equals(name, b.Name, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Battery>> Find(BatteryCriteria battery, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(0, cancellationToken);

        var results = Enumerable.Empty<Battery>();

        if (!string.IsNullOrWhiteSpace(battery.Name))
        {
            var found = _batteries.Where(b => b.Name.Contains(battery.Name, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (!string.IsNullOrWhiteSpace(battery.AnsiName))
        {
            var found = _batteries.Where(b => b.AnsiName.Contains(battery.AnsiName, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (!string.IsNullOrWhiteSpace(battery.IecName))
        {
            var found = _batteries.Where(b => b.IecName.Contains(battery.IecName, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (battery.Voltage > 0)
        {
            var found = _batteries.Where(b => b.Voltage.Equals(battery.Voltage));
            results = results.Concat(found);
        }

        return results.Distinct();
    }

    /// <inheritdoc/>
    public async Task Delete(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(0, cancellationToken);

        var battery = _batteries.FirstOrDefault(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));

        if (battery != null)
        {
            _batteries.Remove(battery);
        }
    }
}
