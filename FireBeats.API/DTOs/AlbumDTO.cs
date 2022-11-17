namespace FireBeats.API.DTOs
{ 
    public record AlbumCreatedDTO(string AlbumName, Guid UserId);
    public record AlbumUpdatedDTO(string AlbumName, Guid UserId);
}
