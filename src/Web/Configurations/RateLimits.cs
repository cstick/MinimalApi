namespace Web.Configurations;

internal record RateLimits
{
    public IEnumerable<RateLimiterPolicy> Policies { get; set; } = [RateLimiterPolicy.Default];
}
