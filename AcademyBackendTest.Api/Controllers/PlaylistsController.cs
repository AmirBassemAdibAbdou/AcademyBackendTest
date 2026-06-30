using Microsoft.AspNetCore.Mvc;
using AcademyBackendTest.Api.DTOs;
using AcademyBackendTest.Api.Services;

namespace AcademyBackendTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _service;

    public PlaylistsController(IPlaylistService service) => _service = service;
    
    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylistDto dto)
    {
        var result = await _service.CreatePlaylistAsync(dto);
        return CreatedAtAction(nameof(GetPlaylists), new { userId = result.UserId }, result);
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetPlaylists(string userId)
    {
        var result = await _service.GetUserPlaylistsAsync(userId);
        return Ok(result);
    }
    [HttpPost("{playlistId}/songs")]
    public async Task<IActionResult> AddSongToPlaylist(Guid playlistId, [FromBody] AddSongDto dto)
    {
        var result = await _service.AddSongToPlaylistAsync(playlistId, dto);
        
        if (result == null)
        {
            // Returns 404 if playlist doesn't exist or user doesn't own it
            return NotFound("Playlist not found or access denied."); 
        }

        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(Guid id, [FromBody] UpdatePlaylistDto dto)
    {
        var success = await _service.UpdatePlaylistAsync(id, dto);
        
        if (!success)
        {
            return NotFound("Playlist not found or access denied.");
        }

        return NoContent(); // Standard HTTP 204 response for a successful update
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(Guid id, [FromQuery] string userId)
    {
        var success = await _service.DeletePlaylistAsync(id, userId);
        
        if (!success)
        {
            return NotFound("Playlist not found or access denied.");
        }

        return NoContent(); // Standard HTTP 204 response for a successful deletion
    }
}