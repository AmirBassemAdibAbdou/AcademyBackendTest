using AcademyBackendTest.Api.DTOs;
using AcademyBackendTest.Api.Models;
using AcademyBackendTest.Api.Repositories;

namespace AcademyBackendTest.Api.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _repository;

    public PlaylistService(IPlaylistRepository repository) => _repository = repository;

    public async Task<PlaylistResponseDto> CreatePlaylistAsync(CreatePlaylistDto dto)
    {
        var playlist = new Playlist 
        { 
            Id = Guid.NewGuid(), 
            Name = dto.Name, 
            UserId = dto.UserId 
        };

        var created = await _repository.CreateAsync(playlist);
        return new PlaylistResponseDto(created.Id, created.Name, created.UserId);
    }

    public async Task<IEnumerable<PlaylistResponseDto>> GetUserPlaylistsAsync(string userId)
    {
        var playlists = await _repository.GetByUserIdAsync(userId);
        return playlists.Select(p => new PlaylistResponseDto(p.Id, p.Name, p.UserId));
    }
}