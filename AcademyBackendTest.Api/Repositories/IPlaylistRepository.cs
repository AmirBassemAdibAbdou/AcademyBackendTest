using AcademyBackendTest.Api.Models;

namespace AcademyBackendTest.Api.Repositories;

public interface IPlaylistRepository
{
    Task<Playlist> CreateAsync(Playlist playlist);
    Task<IEnumerable<Playlist>> GetByUserIdAsync(string userId);
    Task<Playlist?> GetByIdAsync(Guid id);
    Task UpdateAsync(Playlist playlist);
}