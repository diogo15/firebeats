using FireBeats.Context;
using FireBeats.Domain;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Cities>>> GetAsync()
        {
            var cities = await _context.Cities.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, cities);
        }
    }
}
