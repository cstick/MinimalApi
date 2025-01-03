﻿using MediatR;
using Web.Data;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Handles searches for batteries.
/// </summary>
/// <param name="batteries">A repository of batteries.</param>
public class SearchBatteriesHandler(IBatteryRepository batteries) : IRequestHandler<BatteryCriteria, IResult>
{
    /// <inheritdoc/>
    public async Task<IResult> Handle(BatteryCriteria request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var foundBatteries = await batteries.Find(request, cancellationToken);

        return Results.Ok(foundBatteries);
    }
}