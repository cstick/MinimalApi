using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.APIs;
using Web.Models;
using Web.Models.Validators;

namespace Web.Tests.Unit
{
    public class GetWeatherForecastHandlerTests
    {
        private readonly IValidator<GetWeatherForecast> _validator = new GetWeatherForecastValidator();
        private readonly GetWeatherForecastHandler sut;

        public GetWeatherForecastHandlerTests()
        {
            sut = new(_validator);
        }

        [Fact]
        public async Task ReturnsOkayWithForecast()
        {
            var request = new GetWeatherForecast
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
            var request = new GetWeatherForecast
            {
                Id = id,
            };

            var result = (await sut.Handle(request, default)).Result as ValidationProblem;

            Assert.NotNull(result);
        }
    }
}