using Web.Models;

namespace Web.Data;

/// <inheritdoc/>
public class BatteryRepository : IBatteryRepository
{
    private static IList<Battery> _batteries;

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

        _batteries.Add(battery);
    }

    /// <inheritdoc/>
    public void Upsert(Battery battery)
    {
        Delete(battery.Name);
        _batteries.Add(battery);
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

        return results;
    }

    /// <inheritdoc/>
    public void Delete(string name)
    {
        var battery = _batteries.FirstOrDefault(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));

        if (battery != null)
        {
            _batteries.Remove(battery);
        }
    }
}
