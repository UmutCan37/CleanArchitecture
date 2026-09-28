using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Authenticaton
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;
        public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<LoginCommandResponse> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new("fullName", user.FullName)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);



            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshTokenExpires = DateTime.UtcNow.AddDays(_jwtOptions.ExpireDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = refreshTokenExpires;
            await _userManager.UpdateAsync(user);

            return new LoginCommandResponse(
            accessToken,
            refreshToken,
            refreshTokenExpires,
            user.Id,
            user.UserName ?? string.Empty,
            user.FullName);
        }
    }
    
}
