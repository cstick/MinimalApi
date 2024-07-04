using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.APIs;

public static class WeatherApis
{
    public static RouteGroupBuilder MapWeatherApi(this RouteGroupBuilder group)
    {
        group
            .MapGet("{id}", GetWeatherById.Invoke)
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