namespace AcademyBackendTest.Api.DTOs;

public record CreatePlaylistDto(string Name, string UserId);

public record PlaylistResponseDto(Guid Id, string Name, string UserId);