using Microsoft.EntityFrameworkCore;
using AcademyBackendTest.Api.Models;

namespace AcademyBackendTest.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Explicitly configure the many-to-many relationship and name the join table
        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Songs)
            .WithMany(s => s.Playlists)
            .UsingEntity(j => j.ToTable("PlaylistSongs"));
    }
}