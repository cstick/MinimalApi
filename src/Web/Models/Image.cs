namespace Web.Models;

/// <summary>
/// The content of an image.
/// </summary>
public record Image
{
    /// <summary>
    /// The name of a battery.
    /// </summary>
    public required string Battery { get; set; }

    /// <summary>
    /// The file name of the image.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// The media type of the image.
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// The binary content of the image.
    /// </summary>
    public required byte[] Content { get; set; }
}