using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class AuthenticatiomServices
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticatiomServices(UserManager<User> userManager, JwtSettings jwtSettings)
        {
            _userManager=userManager;
            _jwtSettings=jwtSettings;
        }

        public async Task<string> CreateToken(User appUser)
        {

            var claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name , appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier , appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString())
            };


            var roles = await _userManager.GetRolesAsync(appUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var userClaims = await _userManager.GetClaimsAsync(appUser);
            claims.AddRange(userClaims);




            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signincred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            // Create Token
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                 expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: signincred
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;



        }


    }
}
