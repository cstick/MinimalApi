using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.APIs.Groups;

internal static class BatteryApi
{
    public static Task<RouteGroupBuilder> MapBatteryApi(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", (
                [FromServices] IBatteryRepository batteries,
                Battery battery) => CreateBattery.Handle(batteries, battery))
            .WithSummary("Add")
            .WithDescription("Add a battery.");

        group.MapPut(
            "/{name}",
            (IMediator mediator, string name, BatterySpecification specification, CancellationToken cancellationToken) =>
            {
                var request = new PutBattery
                {
                    Name = name,
                    Specification = specification,
                };

                return mediator.Send(request, cancellationToken);
            });

        group.MapGet(
            "/{name}",
            ([FromServices] IMediator mediator, string name, CancellationToken token) => mediator.Send(new GetBatteryByNameRequest { Name = name }, token));

        group.MapPost(
            "/search",
            ([FromServices] IMediator mediator, BatteryCriteria criteria, CancellationToken token) => mediator.Send(criteria, token));

        group.MapDelete("/{name}", (
            string name,
            [FromServices] IBatteryRepository batteries) =>
        {
            batteries.Delete(name);

            return Results.NoContent();
        });

        group.WithOpenApi();

        return Task.FromResult(group);
    }
}
