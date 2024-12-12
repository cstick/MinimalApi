using Web.Data;

namespace Web.APIs;

/// <summary>
/// Handles a <see cref="GetBatteryByNameRequest"/>.
/// </summary>
/// <param name="batteries">A repository of batteries to get from.</param>
public class GetBatteryByNameHandler(IBatteryRepository batteries) : MediatR.IRequestHandler<GetBatteryByNameRequest, IResult>
{
    /// <summary>
    /// Handle the request.
    /// </summary>
    public async Task<IResult> Handle(GetBatteryByNameRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(0, cancellationToken);

        var battery = batteries.Get(request.Name);

        if (battery is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(battery);
    }
}
