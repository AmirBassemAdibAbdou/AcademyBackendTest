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
        return new PlaylistResponseDto(created.Id, created.Name, created.UserId, new List<SongResponseDto>());
    }

    public async Task<IEnumerable<PlaylistResponseDto>> GetUserPlaylistsAsync(string userId)
    {
        var playlists = await _repository.GetByUserIdAsync(userId);
        return playlists.Select(p => new PlaylistResponseDto(
            p.Id, 
            p.Name, 
            p.UserId, 
            p.Songs.Select(s => new SongResponseDto(s.Id, s.Title, s.Artist))
        ));
    }

    public async Task<SongResponseDto?> AddSongToPlaylistAsync(Guid playlistId, AddSongDto dto)
    {
        var playlist = await _repository.GetByIdAsync(playlistId);
        
        // Enforce business rule: Playlist must exist and belong to the requesting user
        if (playlist == null || playlist.UserId != dto.UserId)
        {
            return null;
        }

        var song = new Song 
        { 
            Id = Guid.NewGuid(), 
            Title = dto.Title, 
            Artist = dto.Artist 
        };

        playlist.Songs.Add(song);
        await _repository.UpdateAsync(playlist);

        return new SongResponseDto(song.Id, song.Title, song.Artist);
    }
}