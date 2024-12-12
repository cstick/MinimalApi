using MediatR;

namespace Web.APIs;

/// <summary>
/// Search criteria for batteries.
/// </summary>
public class BatteryCriteria : IRequest<IResult>
{
    /// <summary>
    /// A battery name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// An IEC battery name.
    /// </summary>
    public string IecName { get; set; } = string.Empty;

    /// <summary>
    /// An ANSI battery name.
    /// </summary>
    public string AnsiName { get; set; } = string.Empty;

    /// <summary>
    /// A nominal voltage.
    /// </summary>
    public double Voltage { get; set; } = 0;
}
