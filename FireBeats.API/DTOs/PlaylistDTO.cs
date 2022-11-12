namespace FireBeats.API.DTOs
{ 
    public record PlaylistDTO(Guid id, string listName, Guid userId);
    public record PlaylistCreatedDTO(string userName, Guid songsId, Guid userId);
    public record PlaylistUpdatedDTO(string userName, Guid songsId, Guid userId);
}
