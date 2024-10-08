﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Login")]
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


        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var storedRefreshToken = await _jwtTokenService.GetRefreshTokenAsync(request.RefreshToken);

            if (storedRefreshToken == null || storedRefreshToken.IsUsed || storedRefreshToken.IsRevoked || storedRefreshToken.ExpiryDate < DateTime.Now)
            {
                return Unauthorized("Geçersiz refresh token.");
            }

            // Refresh token geçerli, yeni bir access token oluştur
            var accessToken = _jwtTokenService.GenerateToken(storedRefreshToken.UserId, "KullanıcıAdı"); // Kullanıcı bilgileri veritabanından alınabilir

            // Refresh token'ı işaretleyin (örneğin, bir daha kullanılamayacak şekilde)
            storedRefreshToken.IsUsed = true;
            await _jwtTokenService.Update(storedRefreshToken);

            return Ok(new { AccessToken = accessToken });
        }

        [HttpGet("secret")]
        [Authorize]
        public IActionResult GetSecret()
        {
            return Ok("Bu endpoint'e sadece JWT ile ulaşılabilir!");
        }
    }

}



