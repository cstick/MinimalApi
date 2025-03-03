﻿using System.ComponentModel;
using Web.Data;
using Web.Models;

namespace Web.APIs.Groups;

internal static partial class BatteryImageApis
{
    public static RouteGroupBuilder MapBatteryImageApi(this RouteGroupBuilder group)
    {
        group
            .MapPost("", async Task<IResult> (
                [Description("A battery name.")] string name,
                [Description("The image to add.")] IFormFile file,
                IBatteryRepository batteries,
                IImageRepository images,
                HttpContext context,
                LinkGenerator linkGenerator,
                CancellationToken cancellationToken) =>
            {
                var battery = await batteries.Get(name, cancellationToken);

                if (battery is null)
                {
                    return Results.NotFound("A battery with that name does not exist.");
                }

                using var ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms, cancellationToken);

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
            .WithName(EndpointNames.Batteries.Images.Add)
            .WithSummary("Add battery image.")
            .WithDescription("Add an image to a battery.");

        group
            .MapGet("{id}", async Task<IResult> (
                [Description("A battery name.")] string name,
                [Description("An image id.")] Guid id,
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
            .WithName(EndpointNames.Batteries.Images.Get)
            .WithSummary("Get battery image.")
            .WithDescription("Get a battery image.");

        group.WithOpenApi();

        return group;
    }
}
