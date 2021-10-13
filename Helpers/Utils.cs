using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using ActivityManagementWeb.Dtos;

namespace ActivityManagementWeb.Helpers
{
    public static class Utils
    {
      public static string JwtGenerator(string jwtKey, double jwtExpireDays, string jwtIssuer, StudentDto student)
      {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, student.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, student.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtExpireDays));

        var token = new JwtSecurityToken(
            jwtIssuer,
            jwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
      }
    }
}
