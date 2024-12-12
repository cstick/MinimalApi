using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.APIs.Groups;

internal static class BatteryApi
{
    public static async Task<RouteGroupBuilder> MapBatteryApi(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                Task<IResult> (IMediator mediator, Battery battery, CancellationToken cancellationToken) => mediator.Send(new CreateBatteryRequest(battery), cancellationToken))
            .WithSummary("Add")
            .WithDescription("Add a battery.");

        group.MapPut(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, BatterySpecification specification, CancellationToken cancellationToken) => mediator.Send(new PutBattery { Name = name, Specification = specification, }, cancellationToken));

        group.MapGet(
            "/{name}",
            Task<IResult> (IMediator mediator, string name, CancellationToken token) => mediator.Send(new GetBatteryByNameRequest { Name = name }, token));

        group.MapPost(
            "/search",
            Task<IResult> (IMediator mediator, BatteryCriteria criteria, CancellationToken token) => mediator.Send(criteria, token));

        group.MapDelete(
            "/{name}",
            IResult (string name, [FromServices] IBatteryRepository batteries) =>
            {
                batteries.Delete(name);

                return Results.NoContent();
            });

        group.WithOpenApi();

        return group;
    }
}
