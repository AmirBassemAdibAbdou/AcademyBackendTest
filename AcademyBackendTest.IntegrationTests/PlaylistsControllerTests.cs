using System.Net;
using System.Net.Http.Json;
using AcademyBackendTest.Api.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace AcademyBackendTest.IntegrationTests;

public class PlaylistsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PlaylistsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Set the flag so Program.cs uses the safe testing database
                context.HostingEnvironment.EnvironmentName = "Development";
            });

            builder.ConfigureServices(services =>
            {
                // Force configuration setting dynamically
                var configuration = new Dictionary<string, string> {
                    {"UseInMemoryDatabase", "true"}
                };
                builder.ConfigureAppConfiguration((hostContext, configBuilder) => {
                    configBuilder.AddInMemoryCollection(configuration!);
                });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task CreatePlaylist_ReturnsCreatedStatusAndDto()
    {
        // Arrange
        var request = new CreatePlaylistDto("Workout Mix", "user_1");

        // Act
        var response = await _client.PostAsJsonAsync("/api/playlists", request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<PlaylistResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("Workout Mix", result.Name);
    }
}