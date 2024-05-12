using Microsoft.AspNetCore.HttpLogging;
using Web.Health;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options =>
{
    options.CombineLogs = true;
});

builder.Services.AddMemoryCache();
builder.Logging.AddConsole();


builder.Services.AddHostedService<StartupBackgroundService>();
builder.Services.AddSingleton<StartupHealthCheck>();
builder.Services.AddSingleton<ApiHealthCheck>();

builder.Services
    .AddHealthChecks()
    .AddCheck<StartupHealthCheck>("Startup")
    .AddApiHealthCheck(
        "Cat Facts",
        new Uri("https://cat-fact.herokuapp.com/facts"));

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseExceptionHandler();
//app.UseAuthorization();

var apiGroup = app.MapGroup("/api");

var weatherGroup = apiGroup
    .MapGroup("/weather")
    .WithTags("Weather")
    .WithHttpLogging(HttpLoggingFields.All)
    .MapWeatherApi();

app.MapHealthChecks("/health/live");

app.MapHealthChecks("/health/ready", new()
{
    ResponseWriter = HealthResponse.Writer
});

app.Run();