using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FireBeats.API.Controllers
{
    [Route("api/playlist")]
    [ApiController]
    [Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public PlaylistsController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsyn()
        {
            var playlists = await _context.Playlists
                .Include(p => p.Songs)
                .ToListAsync();
            if (playlists != null)
                return StatusCode(StatusCodes.Status200OK, playlists);

            return StatusCode(StatusCodes.Status404NotFound, "Playlist not found!");
        }

        [HttpGet("{listId}")]
        public async Task<ActionResult> GetByIdAsync(Guid listId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .SingleOrDefaultAsync(p => p.Id == listId);

            if (playlist != null)
                return StatusCode(StatusCodes.Status200OK, playlist);

            return StatusCode(StatusCodes.Status404NotFound, "Playlist not found!");
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetByUserAsync(Guid userId)
        {
            var playlist = _context.Playlists
                .Include(p => p.Songs)
                .Where(p => p.UserId == userId);

            if (playlist != null)
                return StatusCode(StatusCodes.Status200OK, playlist);

            return StatusCode(StatusCodes.Status404NotFound, "User playlists not found!");
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(PlaylistCreatedDTO postedList)
        {
            var user = await _context.Users.FindAsync(postedList.userId);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound);

            var newPlaylist = new Playlists
            {
                Id = Guid.NewGuid(),
                PlaylistName = postedList.listName,
                UserId = postedList.userId,
            };

            try
            {
                _context.Playlists.Add(newPlaylist);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, newPlaylist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, PlaylistUpdatedDTO updatedList)
        {
            var existingList = await _context.Playlists.FindAsync(id);
            if (existingList == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            existingList.PlaylistName = updatedList.listName;
            existingList.UserId = updatedList.userId;

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingList = await _context.Playlists.FindAsync(id);
            if (existingList == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            _context.Playlists.Remove(existingList);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
