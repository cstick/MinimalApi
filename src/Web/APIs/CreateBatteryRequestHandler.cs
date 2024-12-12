using MediatR;
using Web.Data;

namespace Web.APIs;

public class CreateBatteryRequestHandler(IBatteryRepository batteries) : IRequestHandler<CreateBatteryRequest, IResult>
{
    public async Task<IResult> Handle(CreateBatteryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await batteries.DoesBatteryExist(request.Battery.Name))
        {
            return Results.Conflict("Battery already exists.");
        }

        await batteries.AddBattery(request.Battery);

        return Results.Created($"batteries/{request.Battery.Name}", default);
    }
}