using MediatR;
using Web.Models;

namespace Web.APIs;

/// <summary>
/// Create a battery.
/// </summary>
public record CreateBatteryRequest(Battery Battery) : IRequest<IResult>;
