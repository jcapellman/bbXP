using bbxp.web.api.Configuration;
using bbxp.lib.JSON;
using bbxp.lib.Common;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController(IMemoryCache memoryCache, ILogger<AccountController> logger, ApiConfiguration config) : BaseController(memoryCache)
    {
        private readonly ILogger<AccountController> _logger = logger;

        private readonly ApiConfiguration _config = config;

        private string GenerateToken(string hashToken)
        {
            var iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _config.JWTSubject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
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

        [HttpPost]
        public ActionResult<string> Login(UserLoginRequestItem userLogin)
        {
            var hashToken = (userLogin.UserName + userLogin.Password).ToSHA256();

            if (hashToken != _config.JWTHashToken)
            {
                _logger.LogWarning("{hashToken} != {_config.JWTHashToken} - failed login", hashToken, _config.JWTHashToken);

                return Forbid();
            }

            return GenerateToken(hashToken);
        }
    }
}