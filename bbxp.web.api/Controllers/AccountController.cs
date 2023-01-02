using bbxp.lib.Configuration;
using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using bbxp.web.api.Configuration;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;

        private readonly ApiConfiguration _config;

        public AccountController(bbxpDbContext dbContext, IMemoryCache memoryCache, ILogger<AccountController> logger, ApiConfiguration config) : base(dbContext, memoryCache)
        {
            _logger = logger;
            _config = config;
        }

        private string GenerateToken(string hashToken)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _config.JWTSubject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Token", hashToken)
            };

            var token = new JwtSecurityToken(
                _config.JWTIssuer,
                _config.JWTAudience,
                claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.JWTSecret)),
                SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public ActionResult<string> Login(string username, string password)
        {
            var hashToken = (username + password).ToSHA256();

            if (hashToken != _config.JWTHashToken)
            {
                return Forbid();
            }

            return GenerateToken(hashToken);
        }
    }
}