using Microsoft.AspNetCore.Mvc;
using Web.Models;

public static class WeatherApis
{
    public static RouteGroupBuilder MapWeatherApi(this RouteGroupBuilder group)
    {
        group
            .MapGet("/hi/{id}", GetWeatherById.Invoke)
            .WithDescription("With Description")
            .WithSummary("With Summary")
            .WithName("With Name");

        group
            .MapPost("/bye", ([FromBody] GetBye model) => model.WhoName)
            .WithName("foo")
            .WithOpenApi(operation =>
            {
                operation.Deprecated = true;
                return operation;
            });

        group.WithOpenApi();

        return group;
    }
}