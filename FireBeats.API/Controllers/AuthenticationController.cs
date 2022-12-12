using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FireBeats.Domain;
using FireBeats.Context;

namespace FireBeats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;
        private readonly FireBeatsContext _context;

        public AuthenticationController(IConfiguration config, FireBeatsContext context)
        {
            secretKey = config.GetSection("settings").GetSection("secreteKey").ToString();
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CheckUserAsync(string UserName, string UserPassword) {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserPassword))
            {

                var existingUser = _context.Users.Where(u => u.UserName == UserName && u.UserPassword == UserPassword).FirstOrDefault();
                if (existingUser != null) {

                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserName));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string tokenReturn = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new { token = tokenReturn});
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
                }
            }
            else {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = ""});
            }
        }
    }
}
