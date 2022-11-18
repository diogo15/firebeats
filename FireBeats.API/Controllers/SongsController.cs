using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var songs = await _context.Songs.
                ToListAsync();
            if (songs == null)
                return StatusCode(StatusCodes.Status404NotFound, "No songs available!");

            return StatusCode(StatusCodes.Status200OK, songs);
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
                PlaylistId = postedSong.playlistId
            };

            _context.Songs.Add(newSong);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
