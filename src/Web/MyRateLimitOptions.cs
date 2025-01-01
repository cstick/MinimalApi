namespace Web;

/// <summary>
/// Options for rate limiting.
/// </summary>
public class MyRateLimitOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string ConfigurationSection = "MyRateLimit";

    /// <summary>
    /// Replinishment period in seconds.
    /// </summary>
    public int ReplenishmentPeriod { get; set; } = 2;

    /// <summary>
    /// Token limit.
    /// </summary>
    public int QueueLimit { get; set; } = 2;

    /// <summary>
    /// Token limit.
    /// </summary>
    public int TokenLimit { get; set; } = 10;

    /// <summary>
    /// Token limit.
    /// </summary>
    public int TokensPerPeriod { get; set; } = 4;
}