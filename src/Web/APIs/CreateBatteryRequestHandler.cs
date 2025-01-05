using MediatR;
using Web.Data;
using Web.Models.Validators;

namespace Web.APIs;

/// <summary>
/// Handles <see cref="CreateBatteryRequest"/> and returns an <see cref="IResult"/> suitable for a web API.
/// </summary>
/// <param name="batteryValidator"></param>
/// <param name="batteries"></param>
/// <param name="contextAccessor"></param>
/// <param name="linkGenerator">Generates a link for the newly created battery.</param>
public class CreateBatteryRequestHandler(
    BatteryValidator batteryValidator,
    IBatteryRepository batteries,
    IHttpContextAccessor contextAccessor,
    LinkGenerator linkGenerator) : IRequestHandler<CreateBatteryRequest, IResult>
{
    /// <inheritdoc/>
    public async Task<IResult> Handle(CreateBatteryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = await batteryValidator.ValidateAsync(request.Battery, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        };

        if (await batteries.DoesBatteryExist(request.Battery.Name, cancellationToken))
        {
            return Results.Conflict("Battery already exists.");
        }

        await batteries.AddBattery(request.Battery, cancellationToken);

        var context = contextAccessor.HttpContext
            ?? throw new InvalidOperationException("Context does not exist.");

        var url = linkGenerator.GetPathByName(context, "GetBattery", new { request.Battery.Name });

        return Results.Created(url, default);
    }
}
