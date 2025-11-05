using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class RoomsSmokeTests : IClassFixture<WebApplicationFactory<RoomsService.Program>>
{
    private readonly HttpClient _client;

    public RoomsSmokeTests(WebApplicationFactory<RoomsService.Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Endpoint_Returns_OK()
    {
        var response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Rooms_Returns_List_Of_Rooms()
    {
        var response = await _client.GetAsync("/api/rooms");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("Habitación", json);
    }
}
