using System.Net.Http.Json;
using Web.Models;

namespace Web.Tests.Integration;

public class WeatherApiTests
{
    private const string forecastId = "foo";

    private readonly WebApplication host = new();

    [Fact]
    public async Task GetWeatherByIdReturnsRequestedForecast()
    {
        var response = await host.Client.GetAsync($"product/v2/weather/{forecastId}");

        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);

        var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();

        Assert.NotNull(forecast);
        Assert.Equal(forecastId, forecast.Summary);
    }
}