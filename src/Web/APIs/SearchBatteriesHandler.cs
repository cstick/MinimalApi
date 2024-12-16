using MediatR;
using Web.Data;

namespace Web.APIs;

public class SearchBatteriesHandler(IBatteryRepository batteries) : IRequestHandler<BatteryCriteria, IResult>
{
    public async Task<IResult> Handle(BatteryCriteria request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var foundBatteries = await batteries.Find(request, cancellationToken);

        return Results.Ok(foundBatteries);
    }
}