public static class WeatherApis
{
    public static RouteGroupBuilder MapWeatherApi(this RouteGroupBuilder group)
    {
        group
            .MapGet("/hi/{id}", GetWeatherById.Invoke)
            .WithDescription("HIHI")
            .WithSummary("HISUMMARY")
            .WithName("HINAME");

        group
            .MapGet("/bye", () => "SDF")
            .WithName("foo");

        return group;
    }
}