﻿using MediatR;
using Web.Data;
using Web.Models.Validators;

namespace Web.APIs;

public class CreateBatteryRequestHandler(
    BatteryValidator batteryValidator,
    IBatteryRepository batteries,
    IHttpContextAccessor contextAccessor,
    LinkGenerator linkGenerator) : IRequestHandler<CreateBatteryRequest, IResult>
{
    public async Task<IResult> Handle(CreateBatteryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = await batteryValidator.ValidateAsync(request.Battery, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        };

        if (await batteries.DoesBatteryExist(request.Battery.Name))
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