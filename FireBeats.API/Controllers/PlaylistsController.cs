using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.API.Controllers
{
    [Route("api/playlist")]
    [ApiController]
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
                .ToListAsync();
            if (playlists != null)
                return StatusCode(StatusCodes.Status200OK, playlists);

            return StatusCode(StatusCodes.Status404NotFound, "Playlist not found!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var playlist = await _context.Playlists
                .Include(p => p.User)
                .Include(p => p.Albums)
                .Include(p => p.Songs)
                .SingleAsync(p => p.Id == id);

            if (playlist != null)
                return StatusCode(StatusCodes.Status200OK, playlist);

            return StatusCode(StatusCodes.Status404NotFound, "Playlist not found!");
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
        public async Task<ActionResult> PutAsync(Guid id, UserUpdatedDTO updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            existingUser.UserName = updatedUser.userName;
            existingUser.UserEmail = updatedUser.userEmail;
            existingUser.UserPassword = updatedUser.userPassword;
            existingUser.Artist = updatedUser.isArtist;
            existingUser.CitiesId = updatedUser.cityId;

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return StatusCode(StatusCodes.Status404NotFound, "Object not found! :P");

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
