using MediatR;
using Web.Data;
using Web.Models;

namespace Web.APIs;

public class SearchBatteriesHandler(IBatteryRepository batteries) : IRequestHandler<BatteryCriteria, IResult>
{
    public async Task<IResult> Handle(BatteryCriteria request, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);

        var searchBattery = new Battery
        {
            Name = request.Name,
            AnsiName = request.AnsiName,
            IecName = request.IecName,
            Voltage = request.Voltage,
        };

        var foundBatteries = batteries.Find(searchBattery);

        return Results.Ok(foundBatteries);
    }
}