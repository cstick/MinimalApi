using Web.Configurations;

namespace Web.APIs;

/// <summary>
/// Configuration for the API.
/// </summary>
public record APIConfiguration
{
    /// <summary>
    /// The name of the policy to use with the rate limiter.
    /// </summary>
    public string RateLimitertPolicyName { get; set; } = RateLimiterPolicy.Default.PolicyName;
}
