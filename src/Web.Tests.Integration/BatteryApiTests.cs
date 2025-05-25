using System.Net;
using System.Net.Http.Json;
using Web.Models;

namespace Web.Tests.Integration;

public class BatteryApiTests
{
    private readonly WebApplication host = new();

    private readonly Battery battery = new()
    {
        Name = "name",
        AnsiName = "ansi",
        IecName = "iec",
        Voltage = 1.6m,
    };

    [Fact]
    public async Task AddBatteryIsSuccess()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "product/v2/batteries")
        {
            Content = JsonContent.Create(battery)
        };

        var response = await host.Client.SendAsync(request);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.Contains(battery.Name, response.Headers.Location.ToString());
    }

    [Fact]
    public async Task AddBatteryIsInvalid()
    {
        battery.Voltage = 0m;

        var request = new HttpRequestMessage(HttpMethod.Post, "product/v2/batteries")
        {
            Content = JsonContent.Create(battery)
        };

        var response = await host.Client.SendAsync(request);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
