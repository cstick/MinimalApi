using MediatR;
using Web.Models;

namespace Web.Operations;

/// <summary>
/// Create a battery.
/// </summary>
public record CreateBatteryRequest(Battery Battery) : IRequest<IResult>;
