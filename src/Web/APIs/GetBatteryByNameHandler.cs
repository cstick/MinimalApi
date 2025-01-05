using MediatR;
using Web.Data;

namespace Web.APIs;

/// <summary>
/// Handles a <see cref="GetBatteryByName"/>.
/// </summary>
/// <param name="batteries">A repository of batteries to get from.</param>
/// <param name="logger">For logging the operations.</param>
public class GetBatteryByNameHandler(
    IBatteryRepository batteries,
    ILogger<GetBatteryByNameHandler> logger) : IRequestHandler<GetBatteryByName, IResult>
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    public async Task<IResult> Handle(GetBatteryByName request, CancellationToken cancellationToken)
    {
        var battery = await batteries.Get(request.Name, cancellationToken);

        if (battery is null)
        {
            logger.LogCritical("No battery found with name '{Name}'.", request.Name);
            return Results.NotFound();
        }

        logger.LogCritical("Battery found with name '{Name}'.", request.Name);

        return Results.Ok(battery);
    }
}
