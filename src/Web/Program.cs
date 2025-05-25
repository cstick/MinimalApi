using Asp.Versioning;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Web.APIs;
using Web.Data;
using Web.Health;
using Web.Models;
using Web.Operations;

[assembly: InternalsVisibleTo("Web.Tests.Integration")]
[assembly: InternalsVisibleTo("Web.Tests.Unit")]

namespace Web;

/// <summary>
/// Assembly entrypoint.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        }

        builder.AddLogging();

        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new(2);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";

            });

        builder.Services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new()
            {
                Version = "1",
                Title = "Product",
                Description = "An API created to becaome familiar with miminal APIs."
            });

            setup.SwaggerDoc("v2", new()
            {
                Version = "2",
                Title = "Product",
                Description = "An API created to becaome familiar with miminal APIs."
            });
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddApplicationHealthChecks();
        builder.Services.AddHttpClient();

        builder.AddModels();
        builder.AddRepositories();
        builder.AddOperations();
        builder.AddRateLimits();
        builder.AddAPI();

        var app = builder.Build();

        app.AddLogging();
        app.UseHttpsRedirection();
        //app.UseExceptionHandler();
        //app.UseAuthorization();

        app.UseRateLimiter();

        app.AddAPI();

        app.MapProductHealthChecks();

        // Swagger UI.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.EnableTryItOutByDefault();
                options.EnableDeepLinking();
                options.RoutePrefix = "api";
                options.DisplayRequestDuration();
            });
        }

        app.Run();
    }
}
