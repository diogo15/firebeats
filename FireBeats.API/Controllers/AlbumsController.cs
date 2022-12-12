using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FireBeats.API.Controllers
{
    [Route("api/album")]
    [ApiController]
    [Authorize]
    public class AlbumsController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public AlbumsController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var albums = await _context.Albums
                .Include(a => a.Songs)
                .ToListAsync();

            if (albums == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Oops! Item not found");

            return StatusCode(StatusCodes.Status200OK, albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Oops! Item not found");


            return StatusCode(StatusCodes.Status200OK, album);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(AlbumCreatedDTO postedAlbum)
        {
            var newAlbum = new Albums
            { 
                Id = Guid.NewGuid(),
                AlbumName = postedAlbum.AlbumName,
                UserId = postedAlbum.UserId,
            };

            _context.Albums.Add(newAlbum);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, AlbumUpdatedDTO updatedAlbum)
        {
            var getAlbum = await _context.Albums.FindAsync(id);
            if (getAlbum == null)
                return StatusCode(StatusCodes.Status404NotFound, "Oops! Item not found");

            getAlbum.AlbumName = updatedAlbum.AlbumName;
            getAlbum.UserId = getAlbum.UserId;

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        { 
            var getAlbum = await _context.Albums.FindAsync(id);
            if (getAlbum == null)
                return StatusCode(StatusCodes.Status404NotFound, "Oops! Item not found");

            _context.Albums.Remove(getAlbum);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
