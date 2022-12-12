using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FireBeats.API.Domain;
using FireBeats.Context;
using FireBeats.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FireBeats.API.Controllers
{
    [Route("api/genre")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public GenresController(FireBeatsContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genres>> GetByIdAsync(Guid id)
        {
            var genres = await _context.Genres.FindAsync(id);

            if (genres == null)
            {
                return NotFound();
            }

            return genres;
        }

        [HttpPost]
        public async Task<ActionResult<Genres>> PostAsync(GenreCreatedDTO postedGenre)
        {
            var newGenre = new Genres
            { 
                Id = Guid.NewGuid(),
                GenreName = postedGenre.GenreName,
            };

            _context.Genres.Add(newGenre);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(Guid id, GenreUpdatedDTO genreUpdated)
        {
            var getGenre = await _context.Genres.FindAsync(id);
            if (getGenre == null)
                return StatusCode(StatusCodes.Status404NotFound, "No data found");

            getGenre.GenreName = genreUpdated.GenreName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenresExists(Guid id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
