using Microsoft.EntityFrameworkCore;
using AcademyBackendTest.Api.Data;
using AcademyBackendTest.Api.Models;

namespace AcademyBackendTest.Api.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly ApplicationDbContext _context;

    public PlaylistRepository(ApplicationDbContext context) => _context = context;

    public async Task<Playlist> CreateAsync(Playlist playlist)
    {
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();
        return playlist;
    }

    public async Task<IEnumerable<Playlist>> GetByUserIdAsync(string userId)
    {
        return await _context.Playlists
            .Include(p => p.Songs)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
}