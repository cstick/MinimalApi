using MediatR;
using Web.Data;

namespace Web.APIs;

/// <summary>
/// Handles a <see cref="GetBatteryByNameRequest"/>.
/// </summary>
/// <param name="batteries">A repository of batteries to get from.</param>
public class GetBatteryByNameHandler(IBatteryRepository batteries) : IRequestHandler<GetBatteryByNameRequest, IResult>
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    public async Task<IResult> Handle(GetBatteryByNameRequest request, CancellationToken cancellationToken)
    {
        var battery = await batteries.Get(request.Name);

        if (battery is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(battery);
    }
}
