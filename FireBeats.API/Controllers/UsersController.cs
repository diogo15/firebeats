using FireBeats.API.DTOs;
using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly FireBeatsContext _context;

        public UsersController(FireBeatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var users = await _context.Users
                .Include(u => u.Cities.Countries)
                .ToListAsync();

            if (users != null)
                return StatusCode(StatusCodes.Status200OK, users);

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Cities.Countries)
                .SingleAsync(user => user.Id == id);

            if (user != null)
                return StatusCode(StatusCodes.Status200OK, user);

            return StatusCode(StatusCodes.Status404NotFound, "User not found!");
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(UserCreatedDTO postedUser)
        {
            var user = new Users
            {
                Id = Guid.NewGuid(),
                UserName = postedUser.userName,
                UserEmail = postedUser.userEmail,
                UserPassword = postedUser.userPassword,
                Artist = postedUser.isArtist,
                CitiesId = postedUser.cityId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, user);
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
        /*
        [HttpGet("CheckPassword")]
        public async Task<ActionResult> CheckUserAsync(string name, string pass)
        {

            var existingUser = _context.Users.Where(u => u.UserName == name && u.UserPassword == pass).FirstOrDefault();
            if (existingUser == null)
                return NotFound(new { usercheck = "false" });

            return Ok(new { usercheck = "true" });
        }
        */
    }
}
