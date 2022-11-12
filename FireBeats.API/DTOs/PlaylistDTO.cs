namespace FireBeats.API.DTOs
{ 
    public record PlaylistDTO(Guid id, string listName, Guid userId);
    public record PlaylistCreatedDTO(string listName, Guid userId);
    public record PlaylistUpdatedDTO(string listName, Guid userId);
}
