using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireBeats.API.Controllers
{
    [Route("api/users")]
    [ApiController]
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
            var users = await _context.Users.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var user = await _context.Countries.SingleAsync(user => user.Id == id);

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Users postedUser)
        {
            var user = new Users
            {
                Id = Guid.NewGuid(),
                UserName = postedUser.UserName,
                UserEmail = postedUser.UserEmail,
                UserPassword = postedUser.UserPassword,
                Artist = postedUser.Artist,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = user.Id }, user);
        }
    }
}
