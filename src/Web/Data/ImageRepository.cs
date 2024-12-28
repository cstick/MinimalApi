using Web.Models;

namespace Web.Data;

/// <summary>
/// In memory repository of images.
/// </summary>
public class ImageRepository : IImageRepository
{
    /// <summary>
    /// An in memory dictionary of images by their id.
    /// </summary>
    private static readonly Dictionary<(string, Guid), Image> _images = [];

    /// <inheritdoc/>
    public async Task Add(
        (string battery, Guid id) key,
        Image image,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await Task.Delay(30, cancellationToken);

        if (_images.ContainsKey(key))
        {
            throw new InvalidOperationException();
        }

        _images.Add(key, image);
    }

    /// <inheritdoc/>
    public async Task<Image> Get(
        (string battery, Guid id) key,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await Task.Delay(30, cancellationToken);

        if (_images.TryGetValue(key, out Image? image))
        {
            return image;
        }

        throw new InvalidOperationException("Not found.");
    }
}
