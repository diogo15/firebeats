namespace FireBeats.API.DTOs
{ 
    public record SongCreatedDTO(string songname, string songPath, bool isFavorite, Guid genreId, Guid playlistId, Guid albumId);
    public record SongUpdatedDTO(string songname, string songPath, bool isFavorite, Guid genreId, Guid playlistId, Guid albumId);
}
