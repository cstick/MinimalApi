using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;
using Web.Models.Validators;
using Web.Operations;

namespace Web.Tests.Unit;

public class GetWeatherForecastHandlerTests
{
    private readonly GetWeatherForecastValidator _validator = new();
    private readonly GetWeatherHandler sut;

    public GetWeatherForecastHandlerTests()
    {
        sut = new(_validator);
    }

    [Fact]
    public async Task ReturnsOkayWithForecast()
    {
        var request = new GetWeather
        {
            Id = "asd",
        };

        var response = await sut.Handle(request, default);

        Assert.NotNull(response);

        var result = response.Result as Ok<WeatherForecast>;

        Assert.NotNull(result);

        var forecast = result.Value;

        Assert.NotNull(forecast);
        Assert.Equal(request.Id, forecast.Summary);
    }

    [Theory]
    [InlineData("")]
    public async Task ValidatesInput(string id)
    {
        var request = new GetWeather
        {
            Id = id,
        };

        var result = (await sut.Handle(request, default)).Result as ValidationProblem;

        Assert.NotNull(result);
    }
}
