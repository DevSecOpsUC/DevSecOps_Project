using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AuthSmokeTests : IClassFixture<WebApplicationFactory<AuthService.Program>>
{
    private readonly HttpClient _client;

    public AuthSmokeTests(WebApplicationFactory<AuthService.Program> factory)
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
    public async Task Login_Returns_Token_When_Credentials_Valid()
    {
        var login = new { Username = "username", Password = "password" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", login);

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(result?.token);
    }

    [Fact]
    public async Task Login_Returns_401_When_Credentials_Invalid()
    {
        var login = new { Username = "wrong", Password = "bad" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", login);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
