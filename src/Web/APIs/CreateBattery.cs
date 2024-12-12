using Web.Data;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Create a battery.
/// </summary>
public static class CreateBattery
{
    /// <summary>
    /// Add a battery.
    /// </summary>
    public static IResult Handle(
        IBatteryRepository batteries,
        Battery battery)
    {
        if (batteries.DoesBatteryExist(battery.Name))
        {
            return Results.Conflict("Battery already exists.");
        }

        batteries.AddBattery(battery);

        return Results.Created($"batteries/{battery.Name}", default);
    }
}
