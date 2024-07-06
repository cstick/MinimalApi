using Microsoft.AspNetCore.Mvc;

namespace Web.Models;

public record GetWeatherById
{
    public string Id { get; set; } = string.Empty;
}
