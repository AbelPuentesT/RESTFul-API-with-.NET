using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nest;
using SocialMedia.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            
            if (IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new {token});
            }
            return NotFound();

        }
        private bool IsValidUser(UserLogin login) 
        {
            return true;
        }
        private string GenerateToken()
        {
            //Header 
            var _SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_SymmetricSecurityKey,SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"Abel Puentes"),
                new Claim(ClaimTypes.Email,"puentesabel012@gmail.com"),
                new Claim(ClaimTypes.Role,"Admin")
            };
            //Payload
            var payLoad = new JwtPayload
                (
                _configuration["Authentication:Isser"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
                );
            var token = new JwtSecurityToken(header,payLoad);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
