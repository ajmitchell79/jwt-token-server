using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Token.BRL.Common;
using Token.BRL.Interfaces;
using Token.BRL.Model.TokenServer.BRL.Model;

namespace Token.BRL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings, IUserService userRepository)
        {
            _appSettings = appSettings.Value;
            _userService = userRepository;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<TokenResponse> GenerateAccessToken(string fullUsername)
        {
            var login = fullUsername.Split('\\')[1];

            var result = await _userService.GetUser(login); //.Result;

            if (result == null) throw new Exception("User details not found");

            if ((result.Roles == null) || (!result.Roles.Any())) throw new Exception("No roles found for user: " + fullUsername);

            var tokenHandler = new JwtSecurityTokenHandler();

            //Any token created with this key (secret) will be valid for the get / get by id calls
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            //default claims
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, fullUsername),
                    new Claim(ClaimTypes.WindowsUserClaim, result.Guid.ToString()),
                    new Claim(ClaimTypes.Email, result.Email),
                  
                    new Claim(ClaimTypes.Name, result.Name),
                };

            //additional role claims
            foreach (var role in result.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_appSettings.TokenExpiryHours), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = this.GenerateRefreshToken(), //this is currently not hooked up. Look here https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/ for HOWTO
                Type = TokenType.Bearer.ToString(),
                Issued = DateTime.Now.ToString("R"),
                Expires = tokenDescriptor.Expires.Value.ToString("R"),
                ExpiresIn = (int)(DateTime.Now.AddHours(_appSettings.TokenExpiryHours) - DateTime.Now).TotalSeconds //in seconds
            };
        }
    }
}