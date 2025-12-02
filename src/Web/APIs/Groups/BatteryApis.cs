using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;
using Web.Operations;

namespace Web.APIs.Groups;

internal static partial class BatteryApis
{
    public static RouteGroupBuilder MapBatteryApi(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/",
            Task<IResult> (IMediator mediator, Battery battery, CancellationToken cancellationToken) => mediator.Send(new CreateBatteryRequest(battery), cancellationToken))
            .WithSummary("Add a battery.")
            .WithDescription("Add a battery.")
            .WithName(EndpointNames.Batteries.Add);

        group.MapPut(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, BatteryDefinition definition, CancellationToken cancellationToken) => mediator.Send(new PutBattery { Name = name, Definition = definition, }, cancellationToken))
            .WithSummary("Replace a battery.")
            .WithDescription("Replace a battery's specifications.")
            .WithName(EndpointNames.Batteries.Replace);

        group.MapGet(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, CancellationToken token) => mediator.Send(new GetBatteryByName { Name = name }, token))
            .WithSummary("Get a battery.")
            .WithDescription("Get a battery with a given name.")
            .WithName(EndpointNames.Batteries.Get)
            .WithDisplayName("GetBattery");

        group.MapPost(
            "/search",
            Task<IResult> (IMediator mediator, BatteryCriteria criteria, CancellationToken token) => mediator.Send(criteria, token))
            .WithSummary("Search for batteries.")
            .WithDescription("Search batteries by common criteria.")
            .WithName(EndpointNames.Batteries.Search);

        group.MapDelete(
            "/{name}",
            IResult (string name, [FromServices] IBatteryRepository batteries) =>
            {
                batteries.Delete(name);

                return Results.NoContent();
            })
            .WithSummary("Delete a battery")
            .WithDescription("Delete a battery with a given name.")
            .WithName(EndpointNames.Batteries.Delete);

        return group;
    }
}
