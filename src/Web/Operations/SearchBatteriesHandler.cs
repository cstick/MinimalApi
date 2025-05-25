using MediatR;
using Web.Data;
using Web.Models;

namespace Web.Operations;

/// <summary>
/// Handles searches for batteries.
/// </summary>
/// <param name="batteries">A repository of batteries.</param>
internal class SearchBatteriesHandler(IBatteryRepository batteries) : IRequestHandler<BatteryCriteria, IResult>
{
    /// <inheritdoc/>
    public async Task<IResult> Handle(BatteryCriteria request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var foundBatteries = await batteries.Find(request, cancellationToken);

        return Results.Ok(foundBatteries);
    }
}
