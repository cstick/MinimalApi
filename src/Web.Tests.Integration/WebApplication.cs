using Microsoft.AspNetCore.Mvc.Testing;

namespace Web.Tests.Integration;

internal class WebApplication
{
    private readonly WebApplicationFactory<Program> host = new();

    public HttpClient Client
    {
        get { return host.CreateClient(); }
    }
}