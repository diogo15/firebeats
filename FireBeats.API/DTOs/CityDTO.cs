namespace FireBeats.API.DTOs
{
    /*
     * DTO = Data Transfer Object
     * 
     */
    public record CityDTO(Guid Id, string CityName, Guid CountryId);
    public record CityCreatedDTO(string CityName, Guid CountryId);
    public record CityUpdatedDTO(string CityName, Guid CountryId);
}
