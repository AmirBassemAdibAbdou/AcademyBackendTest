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
}