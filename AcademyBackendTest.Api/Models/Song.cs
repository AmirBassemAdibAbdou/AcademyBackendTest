namespace AcademyBackendTest.Api.Models;

public class Song 
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    
    // Navigation property for the many-to-many relationship
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}