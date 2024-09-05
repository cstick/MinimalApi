using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Maps weather APIs.
/// </summary>
internal static class WeatherApis
{
    /// <summary>
    /// Maps the weather APIs to the provided group.
    /// </summary>
    public static RouteGroupBuilder MapWeatherApi(this RouteGroupBuilder group)
    {
        group
            .MapGet("{Id}", ([FromServices] IMediator handler, string id, CancellationToken cancellationToken) =>
            {
                var request = new GetWeatherForecast
                {
                    Id = id,
                };

                return handler.Send(request, cancellationToken);
            })
            .WithDescription("With Description")
            .WithSummary("With Summary")
            .WithName("With Name");

        group
            .MapPost("/search",
            ([FromServices] IMediator handler, [FromBody] SearchWeather searchCriteria, CancellationToken cancellationToken) =>
            handler.Send(searchCriteria, cancellationToken))
            .WithName("search")
            .WithOpenApi();

        group
            .MapPost("/deprecated", ([FromBody] SearchWeather searchCriteria) => searchCriteria.Name)
            .WithName("deprecated")
            .WithOpenApi(operation =>
            {
                operation.Deprecated = true;
                return operation;
            });

        group.WithOpenApi();

        return group;
    }
}