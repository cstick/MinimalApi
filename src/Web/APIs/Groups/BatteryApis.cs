using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

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
            Task<IResult> (IMediator mediator, string name, CancellationToken token) => mediator.Send(new GetBatteryByNameRequest { Name = name }, token))
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

        group.WithOpenApi();

        return group;
    }
}
internal static partial class BatteryImageApis
{
    public static RouteGroupBuilder MapBatteryImageApi(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async Task<IResult> (
                string name,
                IFormFile file,
                IBatteryRepository batteries,
                IImageRepository images,
                HttpContext context,
                LinkGenerator linkGenerator,
                CancellationToken cancellationToken) =>
            {
                var battery = await batteries.Get(name);

                if (battery is null)
                {
                    return Results.NotFound("A battery with that name does not exist.");
                }

                using var ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);

                var id = Guid.NewGuid();
                var image = new Image
                {
                    Battery = name,
                    FileName = Path.GetFileName(file.FileName),
                    ContentType = file.ContentType,
                    Content = ms.ToArray()
                };

                await images.Add((name, id), image, cancellationToken);

                var url = linkGenerator.GetPathByName(
                    context,
                    EndpointNames.Batteries.Images.Get,
                    new { name, id });

                return Results.Created(url, id);
            })
            .DisableAntiforgery()
            .WithName(EndpointNames.Batteries.Images.Add);

        group
            .MapGet("{id}", async Task<IResult> (
                string name,
                Guid id,
                IBatteryRepository batteries,
                IImageRepository images,
                CancellationToken cancellationToken) =>
            {
                var battery = await batteries.Get(name, cancellationToken);

                if (battery is null)
                {
                    return Results.NotFound("A battery with that name does not exist.");
                }

                var image = await images.Get((name, id), cancellationToken);

                if (image is null)
                {
                    return Results.NotFound("The image was not found.");
                }

                return Results.File(image.Content, image.ContentType, image.FileName);
            })
            .WithName(EndpointNames.Batteries.Images.Get);

        group.WithOpenApi();

        return group;
    }
}