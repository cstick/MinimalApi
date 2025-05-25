namespace Web.Configurations;

/// <summary>
/// Options for rate limiting.
/// </summary>
internal class RateLimiterPolicy
{
    /// <summary>
    /// The name of the policy.
    /// </summary>
    public required string PolicyName { get; set; } = "default";

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

    public static RateLimiterPolicy Default { get; } = new()
    {
        PolicyName = "default",
        ReplenishmentPeriod = 2,
        QueueLimit = 2,
        TokenLimit = 10,
        TokensPerPeriod = 4,
    };
}
