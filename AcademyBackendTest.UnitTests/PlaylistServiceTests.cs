using AcademyBackendTest.Api.DTOs;
using AcademyBackendTest.Api.Models;
using AcademyBackendTest.Api.Repositories;
using AcademyBackendTest.Api.Services;
using Moq;

namespace AcademyBackendTest.UnitTests;

public class PlaylistServiceTests
{
    private readonly Mock<IPlaylistRepository> _mockRepo;
    private readonly PlaylistService _service;

    public PlaylistServiceTests()
    {
        _mockRepo = new Mock<IPlaylistRepository>();
        _service = new PlaylistService(_mockRepo.Object);
    }

    [Fact]
    public async Task AddSong_WhenPlaylistDoesNotExist_ReturnsNull()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var dto = new AddSongDto("Bohemian Rhapsody", "Queen", "user_123");
        
        _mockRepo.Setup(repo => repo.GetByIdAsync(playlistId))
                 .ReturnsAsync((Playlist?)null); // Simulate DB returning nothing

        // Act
        var result = await _service.AddSongToPlaylistAsync(playlistId, dto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddSong_WhenUserIsOwner_AddsSongAndReturnsDto()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var userId = "user_123";
        var existingPlaylist = new Playlist { Id = playlistId, Name = "Rock", UserId = userId };
        var dto = new AddSongDto("Bohemian Rhapsody", "Queen", userId);

        _mockRepo.Setup(repo => repo.GetByIdAsync(playlistId))
                 .ReturnsAsync(existingPlaylist);

        // Act
        var result = await _service.AddSongToPlaylistAsync(playlistId, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bohemian Rhapsody", result.Title);
        _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Playlist>()), Times.Once); // Verify DB update was called
    }
}