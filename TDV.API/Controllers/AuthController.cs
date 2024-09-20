using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TDV.Application.Shared.Authentications;
using TDV.Entity.Entities.Authentications;

namespace TDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]  Login request)
        {            
            if (request.Username == "test" && request.Password == "1234")
            {
                var accessToken = _jwtTokenService.GenerateToken("1", request.Username);
                var refreshToken = _jwtTokenService.GenerateRefreshToken();

                // Refresh token'ı veritabanına kaydet
                refreshToken.UserId = "1";
                //await _refreshTokenRepository.SaveRefreshTokenAsync(refreshToken);

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                });
            }

            return Unauthorized();
        }

    }

}



