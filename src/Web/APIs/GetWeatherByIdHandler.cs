using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Get a weather forecast and its ID.
/// </summary>
public class GetWeatherByIdHandler(IValidator<GetWeatherById> validator)
{
    /// <summary>
    /// Blah Blah
    /// </summary>
    /// <param name="request">Parameters for finding the forecast.</param>
    /// <returns></returns>
    public Results<Ok<WeatherForecast>, ValidationProblem> Invoke(GetWeatherById request)
    {
        var validation = validator.Validate(request);

        if (!validation.IsValid)
        {
            return TypedResults.ValidationProblem(validation.ToDictionary());
        }

        return TypedResults.Ok(new WeatherForecast
        {
            Summary = request.Id,
        });
    }
}
