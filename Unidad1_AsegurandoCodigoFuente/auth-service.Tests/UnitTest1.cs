using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Json;


namespace AuthService.Tests;

public class AuthServiceTests : IClassFixture<WebApplicationFactory<AuthService.Program>>
{
    private readonly WebApplicationFactory<AuthService.Program> _factory;

    public AuthServiceTests(WebApplicationFactory<AuthService.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task LoginEndpoint_Returns_Unauthorized_For_InvalidCredentials()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new { Username = "bad", Password = "wrong" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
