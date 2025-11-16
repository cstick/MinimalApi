using MediatR;
using Web.Data;

namespace Web.Operations;

/// <summary>
/// Handles a <see cref="GetBatteryByName"/>.
/// </summary>
/// <param name="batteries">A repository of batteries to get from.</param>
internal class GetBatteryByNameHandler(IBatteryRepository batteries) : IRequestHandler<GetBatteryByName, IResult>
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    public async Task<IResult> Handle(GetBatteryByName request, CancellationToken cancellationToken)
    {
        var battery = await batteries.Get(request.Name, cancellationToken);

        if (battery is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(battery);
    }
}
