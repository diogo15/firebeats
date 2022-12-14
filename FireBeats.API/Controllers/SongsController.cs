using FireBeats.API.Domain;
using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FireBeats.API.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public SongsController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var songs = await _context.Songs
                .Include(s => s.Album)
                .ToListAsync();
            if (songs == null)
                return StatusCode(StatusCodes.Status404NotFound, "No songs available!");

            return StatusCode(StatusCodes.Status200OK, songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var song = await _context.Songs
                .SingleAsync(s => s.Id == id);

            if (song == null)
                return StatusCode(StatusCodes.Status404NotFound, "Song not found");

            return StatusCode(StatusCodes.Status200OK, song);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(SongCreatedDTO postedSong)
        {
            var newSong = new Songs
            {
                Id = Guid.NewGuid(),
                SongName = postedSong.songname,
                SongPath = postedSong.songPath,
                isFavorite = postedSong.isFavorite,
                GenreId = postedSong.genreId,
                AlbumId = postedSong.albumId,
                PlaylistId = null
            };

            _context.Songs.Add(newSong);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, SongUpdatedDTO updatedSong)
        {
            var existingSong = await _context.Songs.FindAsync(id);
            if (existingSong == null) return StatusCode(StatusCodes.Status404NotFound);

            try
            {
                existingSong.PlaylistId = updatedSong.playlistId;
                

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Uops, something went wrong, {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingSong = await _context.Songs.FindAsync(id);
            if (existingSong == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            _context.Songs.Remove(existingSong);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status100Continue);
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Songs>>> Search(string search)
        {
            IQueryable<Songs> query = _context.Songs;
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.SongName.Contains(search));
            }
            return await query.ToListAsync();

        }
    }
}
