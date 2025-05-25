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

        builder.Services.AddMemoryCache();
        builder.Services.AddApplicationHealthChecks();
        builder.Services.AddHttpClient();

        builder.AddLogging();
        builder.AddSwagger();
        builder.AddModels();
        builder.AddRepositories();
        builder.AddOperations();
        builder.AddRateLimits();
        builder.AddAPI();

        var app = builder.Build();

        app.UseLogging();
        app.UseHttpsRedirection();
        //app.UseExceptionHandler();
        //app.UseAuthorization();

        app.UseRateLimiter();

        app.MapAPI();

        app.MapProductHealthChecks();

        if (app.Environment.IsDevelopment())
        {
            app.MapSwagger();
        }

        app.Run();
    }
}
