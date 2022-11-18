namespace FireBeats.API.DTOs
{ 
    public record UserDTO(Guid id, string userName, string userEmail, string userPassword, bool isArtist, Guid cityId);
    public record UserCreatedDTO(string userName, string userEmail, string userPassword, bool isArtist, Guid cityId);
    public record UserUpdatedDTO(string userName, string userEmail, string userPassword, bool isArtist, Guid cityId);
}
