using Web.Models;

namespace Web.Data;

/// <inheritdoc/>
public class BatteryRepository : IBatteryRepository
{
    private static IEnumerable<Battery> _batteries;

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
    public bool DoesBatteryExist(string name)
    {
        return _batteries.Any(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc/>
    public void AddBattery(Battery battery)
    {
        if (_batteries.Any(b => Equals(b.Name, battery.Name)))
        {
            throw new InvalidOperationException("Battery already exists.");
        }

        _batteries = _batteries.Concat([battery]);
    }

    /// <inheritdoc/>
    public Battery? Get(string name)
    {
        return _batteries
            .FirstOrDefault(b => string.Equals(name, b.Name, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc/>
    public IEnumerable<Battery> Find(Battery battery)
    {
        var results = Enumerable.Empty<Battery>();

        if (!string.IsNullOrWhiteSpace(battery.Name))
        {
            var found = _batteries.Where(b => b.Name.Contains(battery.Name, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (!string.IsNullOrWhiteSpace(battery.ANSIName))
        {
            var found = _batteries.Where(b => b.ANSIName.Contains(battery.ANSIName, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (!string.IsNullOrWhiteSpace(battery.IECName))
        {
            var found = _batteries.Where(b => b.IECName.Contains(battery.IECName, StringComparison.OrdinalIgnoreCase));
            results = results.Concat(found);
        }
        if (battery.Voltage > 0)
        {
            var found = _batteries.Where(b => b.Voltage.Equals(battery.Voltage));
            results = results.Concat(found);
        }

        return results;
    }

    /// <inheritdoc/>
    public void Delete(string name)
    {
        _batteries = _batteries.Where(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase) is false);
    }
}
