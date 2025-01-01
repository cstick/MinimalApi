using MediatR;

namespace Web.APIs;

/// <summary>
/// Command to get a battery by name.
/// </summary>
public record GetBatteryByName : IRequest<IResult>
{
    /// <summary>
    /// The name of a battery.
    /// </summary>
    public required string Name { get; set; }
}
