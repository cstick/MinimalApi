using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.APIs;

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

        group.MapPut("/{name}", (
            string name,
            [FromServices] IBatteryRepository batteries,
            IBatterySpecification specification) =>
        {
            var battery = batteries.Get(name);

            if (battery is null)
            {
                return Results.NotFound();
            }

            battery.Voltage = specification.Voltage;

            return Results.Created($"batteries/{battery.Name}", default);
        });

        group.MapGet("/{name}", (
            [FromServices] IBatteryRepository batteries,
            string name) =>
        {
            var battery = batteries.Get(name);

            if (battery is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(battery);
        });

        group.MapPost("/search", (
            [FromServices] IBatteryRepository batteries,
            Battery battery) =>
        {
            return batteries.Find(battery);
        });

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
