using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RoomsService.Tests;

public class RoomsServiceTests : IClassFixture<WebApplicationFactory<RoomsService.Program>>
{
    private readonly WebApplicationFactory<RoomsService.Program> _factory;

    public RoomsServiceTests(WebApplicationFactory<RoomsService.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task RoomsEndpoint_Returns_Success()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/rooms");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
