namespace AcademyBackendTest.Api.Models;

public class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Simulates the user owner to satisfy the "fetch his playlists" requirement
    public string UserId { get; set; } = string.Empty; 

    // Navigation property for the many-to-many relationship
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}