using MediatR;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Validates the request.
/// </summary>
public record PutBattery : IRequest<IResult>
{
    /// <summary>
    /// The name of a battery.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The specifications of a batter.
    /// </summary>
    public required IBatterySpecification Specification { get; set; }
}
