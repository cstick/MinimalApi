using MediatR;
using Web.Data;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Handles the <see cref="PutBattery"/> command.
/// </summary>
/// <param name="validator">Validates the request.</param>
/// <param name="batteries">A repository of batteries.</param>
public class PutBatteryHandler(PutBatteryValidator validator, IBatteryRepository batteries) : IRequestHandler<PutBattery, IResult>
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    public async Task<IResult> Handle(PutBattery request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (validation.IsValid is false)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }

        var battery = await batteries.Get(request.Name, cancellationToken);

        if (battery is null)
        {
            battery = new Battery
            {
                Name = request.Name,
                AnsiName = request.Definition.AnsiName,
                IecName = request.Definition.IecName,
                Voltage = request.Definition.Voltage,
            };

            await batteries.AddBattery(battery, cancellationToken);

            return Results.Created($"batteries/{battery.Name}", battery);
        }

        battery.Voltage = request.Definition.Voltage;
        await batteries.Upsert(battery, cancellationToken);

        return Results.Ok();
    }
}