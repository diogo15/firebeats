﻿using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public CountriesController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var countries = await _context.Countries.ToListAsync();

            if (countries != null)
                return StatusCode(StatusCodes.Status200OK, countries);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var country = await _context.Countries.SingleAsync(country => country.Id == id);

            return StatusCode(StatusCodes.Status200OK, country);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Countries postedCountry)
        {
            var country = new Countries
            {
                Id = Guid.NewGuid(),
                CountryName = postedCountry.CountryName,
                CountryCode = postedCountry.CountryCode,
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, Countries updatedCountry)
        {
            var existingCountry = await _context.Countries.FindAsync(id);
            if (existingCountry == null)
                return StatusCode(StatusCodes.Status404NotFound);

            existingCountry.CountryName = updatedCountry.CountryName;
            existingCountry.CountryCode = updatedCountry.CountryCode;

            return CreatedAtAction(nameof(GetByIdAsync), new { id = existingCountry.Id }, existingCountry);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingCountry = await _context.Countries.FindAsync(id);
            if(existingCountry == null)
                return StatusCode(StatusCodes.Status404NotFound);

            _context.Countries.Remove(existingCountry);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
