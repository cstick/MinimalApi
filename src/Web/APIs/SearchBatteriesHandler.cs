using MediatR;
using Web.Data;
using Web.Models;

namespace Web.APIs;

public class SearchBatteriesHandler(IBatteryRepository batteries) : IRequestHandler<BatteryCriteria, IResult>
{
    public async Task<IResult> Handle(BatteryCriteria request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var searchBattery = new Battery
        {
            Name = request.Name,
            AnsiName = request.AnsiName,
            IecName = request.IecName,
            Voltage = request.Voltage,
        };

        var foundBatteries = await batteries.Find(searchBattery);

        return Results.Ok(foundBatteries);
    }
}