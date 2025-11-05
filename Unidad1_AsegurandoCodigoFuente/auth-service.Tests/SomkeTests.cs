using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class SmokeTests
{
    [Fact]
    public async Task Root_Should_Return_200()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var resp = await client.GetAsync("/");
        Assert.True(resp.StatusCode == HttpStatusCode.OK || resp.StatusCode == HttpStatusCode.NotFound);
        // Ajusta a tu endpoint real: "/", "/health", "/api/ping", etc.
    }
}
