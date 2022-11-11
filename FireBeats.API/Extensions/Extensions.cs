using FireBeats.API.DTOs;
using FireBeats.Domain;

namespace FireBeats.API.Extensions
{
    public static class Extensions
    {
        /*
        public static UserDTO AsDTO(this Users user)
        {
            return new UserDTO(user.Id, user.UserName, user.UserEmail, user.UserPassword, user.Artist, user.Cities.CityName);
        }*/

        public static CountryDTO AsDTO(this Countries country)
        {
            return new CountryDTO(country.Id, country.CountryName, country.CountryCode);
        }

        public static CityDTO AsDTO(this Cities cities)
        {
            return new CityDTO(cities.Id, cities.CityName, cities.Countries.Id);
        }
    }
}
