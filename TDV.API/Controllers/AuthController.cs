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
        public async Task<IActionResult> LoginAsync([FromBody] Login request)
        {
            if (request.Username == "test" && request.Password == "1234")
            {
                var accessToken = _jwtTokenService.GenerateToken("1", request.Username);
                var refreshToken = _jwtTokenService.GenerateRefreshToken();

               
                refreshToken.UserId = "1"; //login olan user id'si Session'dan al
                await _jwtTokenService.SaveRefreshTokenAsync(refreshToken);

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



