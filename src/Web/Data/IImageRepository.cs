using Web.Models;

namespace Web.Data
{
    /// <summary>
    /// A repository of images.
    /// </summary>
    public interface IImageRepository
    {
        /// <summary>
        /// Add an image.
        /// </summary>
        /// <param name="key">The id of an image.</param>
        /// <param name="image">The image to add.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>An awaitable task.</returns>
        Task Add((string battery, Guid id) key, Image image, CancellationToken cancellationToken);

        /// <summary>
        /// Get an image by its id.
        /// </summary>
        /// <param name="key">The id of an image.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>The image if found.</returns>
        /// <exception cref="InvalidOperationException">The image was not found.</exception>
        Task<Image> Get((string battery, Guid id) key, CancellationToken cancellationToken);
    }
}