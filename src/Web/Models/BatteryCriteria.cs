﻿using MediatR;

namespace Web.Models;

/// <summary>
/// Search criteria for batteries.
/// </summary>
public record BatteryCriteria : IRequest<IResult>
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
