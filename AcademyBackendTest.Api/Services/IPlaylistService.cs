using AcademyBackendTest.Api.DTOs;

namespace AcademyBackendTest.Api.Services;

public interface IPlaylistService
{
    Task<PlaylistResponseDto> CreatePlaylistAsync(CreatePlaylistDto dto);
    Task<IEnumerable<PlaylistResponseDto>> GetUserPlaylistsAsync(string userId);
    Task<SongResponseDto?> AddSongToPlaylistAsync(Guid playlistId, AddSongDto dto);
}