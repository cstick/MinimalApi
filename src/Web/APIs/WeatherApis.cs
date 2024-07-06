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
            .MapGet("{Id}", ([FromServices] GetWeatherByIdHandler handler, string id) =>
            {
                var request = new GetWeatherById
                {
                    Id = id,
                };

                return handler.Invoke(request);
            })
            .WithDescription("With Description")
            .WithSummary("With Summary")
            .WithName("With Name");

        group
            .MapPost("/search", ([FromBody] WeatherSearchCriteria searchCriteria) => searchCriteria.Name)
            .WithName("search")
            .WithOpenApi();

        group
            .MapPost("/deprecated", ([FromBody] WeatherSearchCriteria searchCriteria) => searchCriteria.Name)
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