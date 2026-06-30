namespace AcademyBackendTest.Api.DTOs;

public record CreatePlaylistDto(string Name, string UserId);

public record AddSongDto(string Title, string Artist, string UserId);

public record SongResponseDto(Guid Id, string Title, string Artist);

public record PlaylistResponseDto(Guid Id, string Name, string UserId, IEnumerable<SongResponseDto> Songs);