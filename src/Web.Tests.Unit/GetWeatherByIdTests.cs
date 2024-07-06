using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.APIs;
using Web.Models;
using Web.Models.Validators;

namespace Web.Tests.Unit
{
    public class GetWeatherByIdTests
    {
        private readonly IValidator<GetWeatherById> _validator = new GetWeatherByIdValidator();
        private readonly GetWeatherByIdHandler sut;

        public GetWeatherByIdTests()
        {
            sut = new(_validator);
        }

        [Fact]
        public void ReturnsOkayWithForecast()
        {
            var request = new GetWeatherById
            {
                Id = "asd",
            };

            var result = sut.Invoke(request);

            Assert.NotNull(result);
            Assert.IsType<Ok<WeatherForecast>>(result);

            var forecast = result.Value;

            Assert.NotNull(forecast);
            Assert.Equal(request.Id, forecast.Summary);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidatesInput(string id)
        {
            var request = new GetWeatherById
            {
                Id = id,
            };

            Assert.Throws<ValidationException>(() => sut.Invoke(request));
        }
    }
}