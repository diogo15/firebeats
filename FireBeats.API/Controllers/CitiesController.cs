using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public CitiesController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var cities = await _context.Cities
                .Include(c => c.Countries)
                .ToListAsync();

            if (cities != null)
                return StatusCode(StatusCodes.Status200OK, cities);

            return StatusCode(StatusCodes.Status400BadRequest, "Can´t get the data! :(");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var city = await _context.Cities.SingleAsync(city => city.Id == id);

            if (city != null)
                return StatusCode(StatusCodes.Status200OK, city);

            return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CityCreatedDTO postedCities)
        {
            var city = new Cities
            {
                Id = Guid.NewGuid(),
                CityName = postedCities.CityName,
                CountriesId = postedCities.CountryId,
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, city);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, CityUpdatedDTO updatedCity)
        {
            var existingCity = await _context.Cities.FindAsync(id);
            if (existingCity == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            existingCity.CityName = updatedCity.CityName;
            existingCity.CountriesId = updatedCity.CountryId;

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingCountry = await _context.Countries.FindAsync(id);
            if (existingCountry == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            _context.Countries.Remove(existingCountry);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
