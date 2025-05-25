namespace Web.Operations;

internal static class Startup
{
    public static WebApplicationBuilder AddOperations(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<SearchWeatherHandler>());

        return builder;
    }
}
