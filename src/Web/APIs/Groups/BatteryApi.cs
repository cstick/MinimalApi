using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.APIs.Groups;

internal static class BatteryApi
{
    public static RouteGroupBuilder MapBatteryApi(this RouteGroupBuilder group)
    {
        group.MapPost(
            "/",
            Task<IResult> (IMediator mediator, Battery battery, CancellationToken cancellationToken) => mediator.Send(new CreateBatteryRequest(battery), cancellationToken))
            .WithSummary("Add a battery.")
            .WithDescription("Add a battery.")
            .WithName("Add");

        group.MapPut(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, BatterySpecification specification, CancellationToken cancellationToken) => mediator.Send(new PutBattery { Name = name, Specification = specification, }, cancellationToken))
            .WithSummary("Replace a battery.")
            .WithDescription("Replace a battery's specifications.")
            .WithName("Replace");

        group.MapGet(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, CancellationToken token) => mediator.Send(new GetBatteryByNameRequest { Name = name }, token))
            .WithSummary("Get a battery.")
            .WithDescription("Get a battery with a given name.")
            .WithName("Get");

        group.MapPost(
            "/search",
            Task<IResult> (IMediator mediator, BatteryCriteria criteria, CancellationToken token) => mediator.Send(criteria, token))
            .WithSummary("Search for batteries.")
            .WithDescription("Search batteries by common criteria.")
            .WithName("Search");

        group.MapDelete(
            "/{name}",
            IResult (string name, [FromServices] IBatteryRepository batteries) =>
            {
                batteries.Delete(name);

                return Results.NoContent();
            })
            .WithSummary("Delete a battery")
            .WithDescription("Delete a battery with a given name.")
            .WithName("Delete"); 

        group.WithOpenApi();

        return group;
    }
}
