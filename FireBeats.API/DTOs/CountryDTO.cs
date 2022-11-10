namespace FireBeats.API.DTOs
{
    /*
     * DTO = Data Transfer Object
     * 
     */
    public record CountryDTO(Guid id, string countryName, string countryCode);
    public record CountryCreatedDTO(string countryName, string countryCode);
    public record CountryUpdatedDTO(string countryName, string countryCode);
}
