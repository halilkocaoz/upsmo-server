using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UpMo.Entities;
using Microsoft.Extensions.Configuration;
using UpMo.Services.Abstract;
using UpMo.Common.DTO.Response;

namespace UpMo.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration) => 
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"]));

        public Token CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, "UpMo"),
                new Claim(JwtRegisteredClaimNames.Aud, "https://domain.com"),
                new Claim("UserId", user.Id.ToString()),
            };

            var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var expiryDate = DateTime.Now.AddDays(1);

            var tokenValue = new JwtSecurityToken
            (
                expires: expiryDate,
                signingCredentials: signingCredentials,
                claims: claims
            );

            return new Token(new JwtSecurityTokenHandler().WriteToken(tokenValue), expiryDate);
        }
    }
}