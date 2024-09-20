using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TDV.Application.Shared.Authentications;
using TDV.DataAccess.Abstract;
using TDV.DataAccess.Context;
using TDV.DataAccess.Repositories;
using TDV.Entity.Entities.Authentications;

public class JwtTokenService : GenericRepository<RefreshToken>, IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;


    public JwtTokenService(TestDbContext context, IConfiguration configuration, IUnitOfWork unitOfWork) : base(context)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public string GenerateToken(string userId, string username)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Token ID
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiryDate = DateTime.Now.AddDays(7), // 7 gün geçerli olacak.
                IsUsed = false,
                IsRevoked = false
            };
        }
    }

    public async Task SaveRefreshTokenAsync(RefreshToken token)
    {
        await _unitOfWork.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string token)
    {
        return await _unitOfWork.RefreshTokens.Find(x => x.Token == token).SingleOrDefaultAsync();
    }
}
