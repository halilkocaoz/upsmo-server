using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UpMo.Entities;
using Microsoft.Extensions.Configuration;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration) => _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"]));

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, "UpMo"),
                new Claim(JwtRegisteredClaimNames.Aud, "https://domain.com"),
                new Claim("USERID", user.Id.ToString()),
            };
            
            var signCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signCredentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}